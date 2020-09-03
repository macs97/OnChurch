using Microsoft.AspNetCore.Mvc.Rendering;
using OnChurch.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboProfessions();

        IEnumerable<SelectListItem> GetComboCampus();

        IEnumerable<SelectListItem> GetComboSection(int campusId);

        IEnumerable<SelectListItem> GetComboChurch(int sectionId);

    }

}
