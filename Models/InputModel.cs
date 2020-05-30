using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class InputModel
    {
        public IEnumerable<SelectListItem> PostalCodes { get; set; }

        [Required(ErrorMessage = "Please select a Postal Code")]
        public string SelectedPostalCode { get; set; }

        [Required(ErrorMessage = "Please input your annual income")]
        public double AnnualIncome { get; set; }
        public double CalculatedTax { get; set; }

        public StringBuilder ErrorLog { get; set; }
    }
}
