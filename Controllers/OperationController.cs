using Laborlance_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {        
        private readonly IOperationRepository _operationRepo;
        private readonly IAppRepository _appRepo;
        public OperationController(IOperationRepository operationRepo, IAppRepository appRepo)
        {
            _appRepo = appRepo;
            _operationRepo = operationRepo;

        }
    }
}