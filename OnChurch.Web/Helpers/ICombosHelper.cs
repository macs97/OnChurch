using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
