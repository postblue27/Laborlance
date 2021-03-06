using System.Threading.Tasks;
using Laborlance_API.Data;
using Laborlance_API.Dtos;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminrepo;
        private readonly IAppRepository _apprepo;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepo;
        private readonly DataContext _context;
        public AdminController(IAdminRepository adminrepo, IAppRepository apprepo,

            RoleManager<Role> roleManager, UserManager<User> userManager, IUserRepository userRepo, 
            DataContext context)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _adminrepo = adminrepo;
            _apprepo = apprepo;
            _context = context;
        }
    [Authorize(Roles = "Admin")]
    [HttpGet("get-roles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(_roleManager.Roles);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers()
    {
        var usersList = await _adminrepo.GetUsers();
        if (usersList == null)
            return BadRequest("No users yet");
        return Ok(usersList);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("get-users-by-role/{roleName}")]
    public async Task<IActionResult> GetUsersByRole(string roleName)
    {
        var usersList = await _adminrepo.GetUsersByRole(roleName);
        if (usersList == null)
            return BadRequest("No users in this role");
        return Ok(usersList);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("get-users-by-role-for-android/{roleName}")]
    public async Task<IActionResult> GetUsersByRoleForAndroid(string roleName)
    {
        var usersList = await _adminrepo.GetUsersByRoleForAndroid(roleName);
        if (usersList == null)
            return BadRequest("No users in this role");
        return Ok(usersList);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] User userForUpdate)
    {
        var user = await _userManager.FindByIdAsync(userForUpdate.Id.ToString());
        user.UserName = userForUpdate.UserName;
        user.Email = userForUpdate.Email;
        var result = await _userManager.UpdateAsync(user);
        if(result.Succeeded)
        {
            return Ok(user);
        }
        return BadRequest("Problem updating user");
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("update-worker")]
    public async Task<IActionResult> UpdateWorker([FromBody] WorkerForUpdateDto workerForUpdate)
    {
        var worker = await _userRepo.UpdateWorker(workerForUpdate);
        return Ok(worker);
        // if (await _apprepo.SaveAll())
        // {
        //     return Ok(workerForUpdate);
        // }
        // return BadRequest("Problem updating user");
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-user")]
    public async Task<IActionResult> DeleteUser(User userToDelete)
    {
        _apprepo.Delete(userToDelete);
        if (await _apprepo.SaveAll())
        {
            return Ok(userToDelete);
        }
        return BadRequest("Problem deleting user");
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("get-operations")]
    public async Task<IActionResult> GetOperations()
    {
        var operations = _context.Operations;
        return Ok(operations);
    }
}
}