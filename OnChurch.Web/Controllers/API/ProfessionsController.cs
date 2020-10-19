using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnChurch.Web.Data;

namespace OnChurch.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProfessionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProfession()
        {
            return Ok(_context.Professions);
        }
    }
}
