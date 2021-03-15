using System.Threading.Tasks;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        public AppController(RoleManager<Role> roleManager)
        {
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