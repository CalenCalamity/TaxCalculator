using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class InputModel
    {
        public string PostCode { get; set; }
        public double AnnualIncome { get; set; }
        public double CalculatedTax { get; set; }
    }
}
