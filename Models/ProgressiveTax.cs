using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class ProgressiveTax
    {
        [Display(Name = "Progressive Tax ID")]
        public int ProgressiveTaxID { get; set; }

        [Display(Name = "Tax Rate")]
        [Required(ErrorMessage = "The tax rate is required")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:P2}")]
        public double Rate { get; set; }

        [Required(ErrorMessage = "The start of the tax bracket is required")]
        [Display(Name = "Tax Bracket Start")]
        public double From { get; set; }

        [Required(ErrorMessage = "The end of the tax bracket is required")]
        [Display(Name = "Tax Bracket End")]
        public double To { get; set; }


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
