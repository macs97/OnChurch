using Microsoft.AspNetCore.Http;
using OnChurch.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Models
{
    public class MemberViewModel : Member
    {
        [Display(Name = "Photograph")]
        public IFormFile PhotoFile { get; set; }
    }
}
