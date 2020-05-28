using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaxCalculator.Data;
using TaxCalculator.Controllers;
using TaxCalculator.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace TaxCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  TaxCalculatorContext _context;

        public HomeController(ILogger<HomeController> logger, TaxCalculatorContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            #region ViewBag 

            List<SelectListItem> postCodes = new List<SelectListItem>();
            _context.PostalCodes.ToList<PostalCode>().ForEach(x => postCodes.Add(new SelectListItem { Text = x.Value, Value = x.Value.ToString() }));

            ViewBag.PostCodes = postCodes;

            #endregion

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CalculateTax(InputModel model)
        {
            string postalCode = model.PostCode;
            double annualIncome = model.AnnualIncome;

            StringBuilder errorLog = new StringBuilder();
            double calculatedTax = 0;

            try
            {
                switch (_context.TaxTypes.Where(x => x.TaxTypeID.Equals(_context.PostalCodes.Where(x => postalCode.Equals(x.Value)).FirstOrDefault().TaxTypeID)).FirstOrDefault().Code)
                {
                    case "FLVL":
                        calculatedTax = annualIncome < 200000 ? calculatedTax = annualIncome * 0.05 : 10000;
                        break;

                    case "FLRT":
                        calculatedTax = annualIncome * 0.175;
                        break;

                    case "PGRSV":
                        IList<ProgressiveTax> ApplicableTaxes = _context.ProgressiveTaxes.ToList().Where(x => x.From < annualIncome).OrderByDescending(x => x.To).ToList<ProgressiveTax>();

                        bool maxRate = true;
                        List<double> finalList = new List<double>();

                        foreach (ProgressiveTax item in ApplicableTaxes)
                        {
                            if (maxRate)
                            {
                                double toAdd = ApplicableTaxes.Count == 1 ? (annualIncome - item.From) * item.Rate : ((annualIncome - (item.From - 1)) * item.Rate);
                                finalList.Add(toAdd);
                                maxRate = false;
                            }
                            else
                            {
                                if (item.From == 0)
                                    finalList.Add(item.To * item.Rate);
                                else
                                    finalList.Add((item.To - (item.From - 1)) * item.Rate);
                            }
                        }

                        calculatedTax = Math.Round(finalList.Sum(), 2);
                        break;

                    default:
                        break;

                }
            }
            catch (Exception e) { errorLog.Append(e.InnerException); }

            _context.CalculatedTaxes.Add(new CalculatedTax { PostalCode = _context.PostalCodes.Where(x => x.Value == postalCode).FirstOrDefault(), Value = calculatedTax, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = DateTime.Now, LastModifiedDate = DateTime.Now });
            _context.SaveChanges();

            ViewData["calculatedTax"] = calculatedTax;

            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
