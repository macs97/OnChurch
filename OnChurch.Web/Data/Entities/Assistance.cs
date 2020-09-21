using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Data.Entities
{
    public class Assistance
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Meeting Meeting { get; set; }

        [Display(Name = "Is Present")]
        public bool IsPresent { get; set; }
    }

}
