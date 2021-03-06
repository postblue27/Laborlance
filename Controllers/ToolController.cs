using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Dtos;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Laborlance_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly IToolRepository _toolRepo;
        private readonly IAppRepository _appRepo;
        private readonly CloudinaryService _cloudinaryService;

        public ToolController(IToolRepository toolRepo, IAppRepository appRepo, CloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
            _appRepo = appRepo;
            _toolRepo = toolRepo;
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
            List<string> publicIds = new List<string>();
            foreach(var toolImage in toolToDelete.ToolImages)
            {
                publicIds.Add(toolImage.PublicId);
            }
            var imagesDeletionResult = await _cloudinaryService.DeleteImagesAsync(publicIds);
            // if(!imagesDeletionResult.IsCompletedSuccessfully)
            //     return BadRequest(imagesDeletionResult.IsCompletedSuccessfully);
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
        
        [Authorize(Roles = "Renter")]
        [HttpPost("add-tool")]
        public async Task<IActionResult> AddTool([FromForm]IFormFile[] images, [FromForm] string toolName, [FromForm] double rentalPrice, [FromForm] int renterId)
        {
            
            // return Ok(images.Length);
            Tool toolToCreate = new Tool{
              ToolName = toolName,
              RentalPrice = rentalPrice,
              RenterId = renterId  
            };
            _appRepo.Add(toolToCreate);
            if(!await _appRepo.SaveAll())
            {
                return BadRequest("Problem adding tool");
            }  

            foreach (var i in images)
            {
                var result = await _cloudinaryService.UploadImageAsync(i);
                // return Ok(result);
                if (result.Error != null) return BadRequest(result.Error.Message);
                ToolImage toolImage = new ToolImage{
                    ToolId = toolToCreate.ToolId,
                    ImageUrl = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId
                };
                _appRepo.Add(toolImage);
                if(!await _appRepo.SaveAll())
                {
                    return BadRequest("Problem adding photo");
                }  
            }
            return Ok(toolToCreate);
        }

        [Authorize(Roles = "Renter,Admin")]
        [HttpPost("edit-tool")]
        public async Task<IActionResult> EditTool([FromForm]int toolId, [FromForm]string toolName, [FromForm]double rentalPrice)
        {
            var tool = await _toolRepo.GetToolById(toolId);
            tool.ToolName = toolName;
            tool.RentalPrice = rentalPrice;
            await _appRepo.SaveAll();
            return Ok(tool);
        }        

        [Authorize(Roles = "Renter,Admin")]
        [HttpPost("add-new-tool-images")]
        public async Task<IActionResult> AddNewToolImages([FromForm] IFormFile[] images, [FromForm]int toolId)
        {
            var tool = await _toolRepo.GetToolById(toolId);
            foreach (var i in images)
            {
                var result = await _cloudinaryService.UploadImageAsync(i);
                // return Ok(result);
                if (result.Error != null) return BadRequest(result.Error.Message);
                ToolImage toolImage = new ToolImage{
                    ToolId = tool.ToolId,
                    ImageUrl = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId
                };
                _appRepo.Add(toolImage);
            }
            if(!await _appRepo.SaveAll())
            {
                return BadRequest("Problem adding photo");
            } 
            return Ok(tool);
        }

        [Authorize(Roles = "Renter,Admin")]
        [HttpGet("get-tool-by-id/{toolId}")]
        public async Task<IActionResult> GetToolById(int toolId)
        {
            var tool = await _toolRepo.GetToolById(toolId);
            return Ok(tool);
        }

        [Authorize(Roles = "Renter,Admin")]
        [HttpDelete("delete-tool-image/{publicId}")]
        public async Task<IActionResult> DeleteToolImage(string publicId)
        {
            var toolImage = await _toolRepo.GetToolImageByPublicIdAsync(publicId);
            var toolId = toolImage.ToolId;
            var imageList = new List<string>();
            imageList.Add(toolImage.PublicId);
            var imageDeletionResult = await _cloudinaryService.DeleteImagesAsync(imageList);
            _appRepo.Delete(toolImage);
            await _appRepo.SaveAll();
            var tool = await _toolRepo.GetToolById(toolId);
            return Ok(tool);
        }        

    }
}