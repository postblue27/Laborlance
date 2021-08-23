using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Laborlance_API.Dtos;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Laborlance_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly TokenService _tokenService;
        public AuthController(IAuthRepository repo, IConfiguration config,
            UserManager<User> userManager, SignInManager<User> signInManager,
        IMapper mapper, RoleManager<Role> roleManager, TokenService tokenService)
        {
            _tokenService = tokenService;
            _roleManager = roleManager;
            _mapper = mapper;
            _repo = repo;
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
        }
    //TODO: only let admins create new admin accounts
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
        if (!await _roleManager.RoleExistsAsync(userForRegisterDto.RoleName))
            return BadRequest("Provided user role does not exist");
        // var userToCreate = _mapper.Map<User>(userForRegisterDto);
        var userToCreate = new User {};
        var roleName = userForRegisterDto.RoleName.ToLower();
        switch(roleName)
        {
            case "worker":
                userToCreate = _mapper.Map<Worker>(userForRegisterDto);
                break;
            case "customer":
                userToCreate = _mapper.Map<Customer>(userForRegisterDto);
                break;
            case "renter":
                userToCreate = _mapper.Map<Renter>(userForRegisterDto);
                break;
            case "admin":
                userToCreate = _mapper.Map<User>(userForRegisterDto);
                break;
            default:
                break;
        }
        // var userToCreate = _mapper.Map<Worker>(userForRegisterDto);
        var userCreationResult = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);
        if (!userCreationResult.Succeeded)
            return BadRequest(userCreationResult.Errors);
        var roleAdditionResult = await _userManager.AddToRoleAsync(userToCreate, userForRegisterDto.RoleName);
        if (!roleAdditionResult.Succeeded)
            return BadRequest(roleAdditionResult.Errors);

        return Ok(userToCreate);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
        var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
        if (user == null)
            return Unauthorized("User with provided username not found");

        var result = await _signInManager.CheckPasswordSignInAsync(user,
            userForLoginDto.Password, false);

        if (result.Succeeded)
        {
            if (!await _userManager.IsInRoleAsync(user, userForLoginDto.RoleName))
                return Unauthorized("User does not have access to this role");

            return Ok(new
            {
                token = await _tokenService.GenerateJwtToken(user),
                user
            });
        }
        return Unauthorized("Problem logging in");
    }

}
}