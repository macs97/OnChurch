using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnChurch.Common.Entities;
using OnChurch.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnChurch.Web.Models
{
    public class EditMemberViewModel : Member
    {
        [Display(Name = "Photograph")]
        public IFormFile PhotoFile { get; set; }

        [Display(Name = "Profession")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a profession.")]
        [Required]
        public int ProfessionId { get; set; }

        public IEnumerable<SelectListItem> Professions { get; set; }

        [Display(Name = "Campus")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a campus.")]
        [Required]
        public int CampusId { get; set; }

        public IEnumerable<SelectListItem> Campuses { get; set; }

        [Display(Name = "Section")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a section.")]
        [Required]
        public int SectionId { get; set; }

        public IEnumerable<SelectListItem> Sections { get; set; }

        [Display(Name = "Church")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a church.")]
        [Required]
        public int ChurchId { get; set; }

        public IEnumerable<SelectListItem> Churches { get; set; }
    }
}
