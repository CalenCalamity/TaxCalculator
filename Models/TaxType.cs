using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class TaxType
    {
        [Display(Name = "Tax Type Id")]
        public int TaxTypeID { get; set; }

        [Required(ErrorMessage = "The Tax Type Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Tax Type Code is required")]
        [Display(Name = "Code")]
        public string Code { get; set; }

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
