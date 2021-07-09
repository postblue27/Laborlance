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
                return Ok(toolToCreate);
            return BadRequest("Problem adding tool");
        }

        [Authorize(Roles = "Renter,Admin")]
        [HttpGet("get-renter-tools/{renterId}")]
        public async Task<IActionResult> GetRenterTools(int renterId)
        {
            var renterTools = await _toolRepo.GetRenterTools(renterId);
            return Ok(renterTools);
        }
        [Authorize(Roles = "Admin,Renter")]
        [HttpDelete("delete-tool/{toolId}")]
        public async Task<IActionResult> DeleteTool(int toolId)
        {
            var toolToDelete = await _toolRepo.GetToolById(toolId);
            // return Ok(toolToDelete);
            _appRepo.Delete(toolToDelete);
            if (await _appRepo.SaveAll())
            {
                return Ok(toolToDelete);
            }
            return BadRequest("Problem deleting tool");
        }
        [Authorize(Roles = "Renter,Admin,Worker")]
        [HttpGet("get-all-tools")]
        public async Task<IActionResult> GetAllTools()
        {
            var tools = await _toolRepo.GetAllTools();
            return Ok(tools);
        }
    }
}