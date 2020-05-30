using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class CalculatedTax
    {
        [Display(Name = "Calculated Tax ID")]
        public int CalculatedTaxID { get; set; }
        [Display(Name = "Calculated Tax")]
        public double Value { get; set; }
        [Display(Name = "Postal Code")]
        public PostalCode PostalCode { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }
    }
}
