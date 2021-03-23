using System.Threading.Tasks;
using Laborlance_API.Data;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        public AppController(RoleManager<Role> roleManager, UserManager<User> userManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    [HttpGet("get-roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = _roleManager.Roles;
        return Ok(roles);
    }
    
}
}