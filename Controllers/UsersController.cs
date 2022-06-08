using AnchTestBackend.Data;
using AnchTestBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnchTestBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UsersController(UserDbContext userDbContext) => _context = userDbContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context == null)
                return NotFound();

            return await _context.Data.ToListAsync();
        }

        [HttpGet("filterUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetFilteredUsers([FromQuery] User parameter)
        {
            if (_context == null)
                return NotFound();

            var userData = await _context.Data.Where(x => (x.Role == parameter.Role
                                                     || x.JobTitle == parameter.JobTitle
                                                     || x.OrganisationUnit == parameter.OrganisationUnit
                                                     || x.FirstName == parameter.FirstName
                                                     || x.LastName == parameter.LastName
                                                     || x.Email == parameter.Email)).ToListAsync();
            if (userData == null)
                return NotFound();

            return userData;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {
            if (_context == null)
                return NotFound();
            List<string> AllRoles = new List<string>();
            List<User> userRole = await _context.Data.GroupBy(p => p.Role).Select(g => g.First()).ToListAsync();

            foreach (var item in userRole)
                AllRoles.Add(item.Role);
            return AllRoles;
        }

        [HttpGet("jobtitles")]
        public async Task<ActionResult<List<string>>> GetJobTitles()
        {
            if (_context == null)
                return NotFound();
            List<string> JobTitles = new List<string>();
            List<User> userData = await _context.Data.GroupBy(p => p.JobTitle).Select(g => g.First()).ToListAsync();

            foreach (var item in userData)
                JobTitles.Add(item.JobTitle);
            return JobTitles;
        }

        [HttpGet("organisationunits")]
        public async Task<ActionResult<List<string>>> GetOrganisationUnits()
        {
            if (_context == null)
                return NotFound();
            List<string> OrganisationUnits = new List<string>();
            List<User> userData = await _context.Data.GroupBy(p => p.OrganisationUnit).Select(g => g.First()).ToListAsync();

            foreach (var item in userData)
                OrganisationUnits.Add(item.OrganisationUnit);
            return OrganisationUnits;
        }

    }
}
