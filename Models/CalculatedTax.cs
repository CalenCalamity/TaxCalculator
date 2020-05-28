using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class CalculatedTax
    {
        public int CalculatedTaxID { get; set; }
        public double Value { get; set; }
        public PostalCode PostalCode { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
