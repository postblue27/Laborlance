using System.Threading.Tasks;
using Laborlance_API.Dtos;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Laborlance_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly IProposalRepository _proposalRepo;
        private readonly IAppRepository _appRepo;
        private readonly BestChoiceService _bcService;
        public ProposalController(IProposalRepository proposalRepo, IAppRepository appRepo,
            BestChoiceService bcService)
        {
            _bcService = bcService;
            _appRepo = appRepo;
            _proposalRepo = proposalRepo;

        }

    [Authorize(Roles = "Worker")]
    [HttpPost("add-proposal")]
    public async Task<IActionResult> AddProposal(ProposalForCreateDto proposalDto)
    {
        Proposal proposalForCreate = new Proposal
        {
            OrderId = proposalDto.OrderId,
            WorkerId = proposalDto.WorkerId
        };
        _appRepo.Add(proposalForCreate);
        await _appRepo.SaveAll();
        int j = 0;
        var i = proposalDto.ToolsInProposal.GetEnumerator();
        while (j < proposalDto.ToolsInProposal.Count)
        {
            i.MoveNext();
            ToolInProposal tip = new ToolInProposal
            {
                ProposalId = proposalForCreate.ProposalId,
                ToolId = i.Current.ToolId
            };
            _appRepo.Add(tip);
            j++;
        }
        i.MoveNext();
        if (await _appRepo.SaveAll())
        {
            return Ok(proposalForCreate);
            // return Ok(i.Current);
        }
        return BadRequest("Problem adding proposal");
    }

    // [Authorize(Roles = "Worker,Admin")]
    [HttpGet("get-order-proposals/{orderId}")]
    public async Task<IActionResult> GetOrderProposals(int orderId)
    {
        var orderProposals = await _proposalRepo.GetOrderProposals(orderId);
        return Ok(orderProposals);
    }

    [Authorize(Roles = "Worker,Admin")]
    [HttpGet("get-worker-proposals/{workerId}")]
    public async Task<IActionResult> GetWorkerProposals(int workerId)
    {
        var proposals = await _proposalRepo.GetWorkerProposals(workerId);
        return Ok(proposals);
    }

    [HttpGet("get-proposal-rating/{orderId}")]
    public async Task<IActionResult> GetProposalRating(int orderId)
    {
        var result = await _bcService.ProposalRating(orderId);
        return Ok(result);
    }
}
}