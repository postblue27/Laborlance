using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly IToolRepository _toolRepo;
        private readonly IAppRepository _appRepo;

        public ToolController(IToolRepository toolRepo, IAppRepository appRepo)
        {
            _appRepo = appRepo;
            _toolRepo = toolRepo;
        }
        
        [Authorize(Roles = "Renter")]
        [HttpPost("add-tool")]
        public async Task<IActionResult> AddTool(Tool toolToCreate)
        {
            _appRepo.Add(toolToCreate);
            if(await _appRepo.SaveAll())
                return Ok("Tool succsessfully added");
            return BadRequest("Problem adding tool");
        }

        [Authorize(Roles = "Renter,Admin")]
        [HttpGet("get-renter-tools/{renterId}")]
        public async Task<IActionResult> GetRenterTools(int renterId)
        {
            var renterTools = await _toolRepo.GetRenterTools(renterId);
            return Ok(renterTools);
        }
    }
}