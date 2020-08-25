﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnChurch.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnChurch.Web.Models
{
    public class MemberViewModel : Member
    {
        [Display(Name = "Photograph")]
        public IFormFile PhotoFile { get; set; }

        [Display(Name = "Profession")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a category.")]
        [Required]
        public int ProfessionId { get; set; }

        public IEnumerable<SelectListItem> Professions { get; set; }
    }
}