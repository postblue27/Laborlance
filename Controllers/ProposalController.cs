using Laborlance_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly IProposalRepository _proposalRepo;
        private readonly IAppRepository _appRepo;
        public ProposalController(IProposalRepository proposalRepo, IAppRepository appRepo)
        {
            _appRepo = appRepo;
            _proposalRepo = proposalRepo;

        }

    }
}