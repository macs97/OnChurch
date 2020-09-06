using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Web.Data;

namespace OnChurch.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusesController : ControllerBase
    {
        private readonly DataContext _context;

        public CampusesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCampus()
        {
            return Ok(_context.Campuses.Include(c => c.Sections)
                .ThenInclude(d => d.Churches));
        }
    }

}
