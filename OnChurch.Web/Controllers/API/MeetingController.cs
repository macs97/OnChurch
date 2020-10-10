using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnChurch.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public MeetingController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        //[Route("GetMeetings")]
        public async Task<IActionResult> GetMeeting()
        {
            /*string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }
            User user = await _userHelper.GetMemberAsync(userId);
            if (user == null)
            {
                return NotFound();
            }*/

            List<Meeting> meeting = await _context.Meetings
                .Include(m => m.Assistances)
                .ThenInclude(a => a.User).ToListAsync();
                //.Where(m => m.Church == user.Church).ToListAsync();
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting); 
        }
    }
}
