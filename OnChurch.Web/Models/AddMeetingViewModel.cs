using OnChurch.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Models
{
    public class AddMeetingViewModel
    {

        [Required]
        [Display(Name = "ChurchId")]
        public int ChurchId { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime Date { get; set; }

        public ICollection<Assistance> Assistances { get; set; }
    }
}
