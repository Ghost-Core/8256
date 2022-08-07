using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab7.Models.EntityMetadata
{
    public class EmployeeMetaData
    {

        [Display(Name = "Employee ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required!")]
        [RegularExpression(@"[a-zA-Z]+\s+[a-zA-Z]+")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User name is required!")]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "User name length should be more than 3 characters")]
        [Display(Name = "Network Id")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password length should be more than 5 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
