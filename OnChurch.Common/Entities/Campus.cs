using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnChurch.Common.Entities
{
    public class Campus
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; }

        [DisplayName("Section Number")]
        public int SectionsNumber => Sections == null ? 0 : Sections.Count;
    }
}
