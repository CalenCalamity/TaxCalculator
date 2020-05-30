using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class PostalCode
    {
        public int PostalCodeID { get; set; }

        [Required(ErrorMessage = "The Post Code is required")]
        [Display(Name = "Post Code")]
        public string Value { get; set; }

        [Required(ErrorMessage = "The Tax Type is required")]
        [Display(Name = "Tax Type")]
        public int TaxTypeID { get; set; }

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
