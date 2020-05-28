using Microsoft.AspNetCore.Razor.Language.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaxCalculator.Models;

namespace TaxCalculator.Data
{
    public class DbInitializer
    {

        public static void Initialize(TaxCalculatorContext context)
        {
            context.Database.EnsureCreated();

            if (context.TaxTypes.Any())
                return;// DB has been seeded

            var progressiveTaxes = new ProgressiveTax[]
            {
                new ProgressiveTax{ Rate=0.1, From=0, To=8350, CreatedBy="System", LastModifiedBy="System", CreatedDate=DateTime.Now, LastModifiedDate=DateTime.Now},
                new ProgressiveTax{ Rate=0.15, From=8351, To=33950, CreatedBy="System", LastModifiedBy="System", CreatedDate=DateTime.Now, LastModifiedDate=DateTime.Now},
                new ProgressiveTax{ Rate=0.25, From=33951, To=82250, CreatedBy="System", LastModifiedBy="System", CreatedDate=DateTime.Now, LastModifiedDate=DateTime.Now},
                new ProgressiveTax{ Rate=0.28, From=82251, To=171550, CreatedBy="System", LastModifiedBy="System", CreatedDate=DateTime.Now, LastModifiedDate=DateTime.Now},
                new ProgressiveTax{ Rate=0.33, From=171551, To=372950, CreatedBy="System", LastModifiedBy="System", CreatedDate=DateTime.Now, LastModifiedDate=DateTime.Now},
                new ProgressiveTax{ Rate=0.35, From=372951, To=double.MaxValue, CreatedBy="System", LastModifiedBy="System", CreatedDate=DateTime.Now, LastModifiedDate=DateTime.Now}
            };

            progressiveTaxes.ToList().ForEach(x => context.ProgressiveTaxes.Add(x));
            context.SaveChanges();

            var taxTypes = new TaxType[]
            {
                new TaxType{ Description="Progressive", Code="PGRSV", CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                new TaxType{ Description="Flat Value", Code="FLVL", CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                new TaxType{ Description="Flat Rate", Code="FLRT", CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now}
            };

            taxTypes.ToList().ForEach(x => context.TaxTypes.Add(x));
            context.SaveChanges();

            var postalCodes = new PostalCode[]
            {
                new PostalCode{ Value="7441", TaxTypeID = context.TaxTypes.ToList().Where(x => x.Code.Equals("PGRSV")).FirstOrDefault().TaxTypeID, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                new PostalCode{ Value="A100", TaxTypeID = context.TaxTypes.ToList().Where(x => x.Code.Equals("FLVL")).FirstOrDefault().TaxTypeID, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                new PostalCode{ Value="7000", TaxTypeID = context.TaxTypes.ToList().Where(x => x.Code.Equals("FLRT")).FirstOrDefault().TaxTypeID, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                new PostalCode{ Value="1000", TaxTypeID = context.TaxTypes.ToList().Where(x => x.Code.Equals("PGRSV")).FirstOrDefault().TaxTypeID, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now}
            };

            postalCodes.ToList().ForEach(x => context.PostalCodes.Add(x));
            context.SaveChanges();
        }
    }
}
