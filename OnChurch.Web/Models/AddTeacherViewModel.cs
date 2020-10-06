using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Models
{
    public class AddTeacherViewModel : EditMemberViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [EmailAddress]
        public string Username { get; set; }
    }
}
