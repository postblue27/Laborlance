using Laborlance_API.Interfaces;
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
    }
}