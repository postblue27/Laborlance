using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IAppRepository _appRepo;
        public ReviewController(IAppRepository appRepo, IReviewRepository reviewRepo)
        {
            _appRepo = appRepo;
            _reviewRepo = reviewRepo;

        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("make-review")]
        public async Task<IActionResult> MakeReview(Review reviewToCreate)
        {
            _appRepo.Add(reviewToCreate);
            if(await _appRepo.SaveAll())
                return Ok();
            return BadRequest("Problem creating review");
        }
    }
}