using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationRepository _operationRepo;
        private readonly IAppRepository _appRepo;
        private readonly IProposalRepository _proposalRepo;
        public OperationController(IOperationRepository operationRepo, IAppRepository appRepo,
            IProposalRepository proposalRepo)
        {
            _proposalRepo = proposalRepo;
            _appRepo = appRepo;
            _operationRepo = operationRepo;
        }

    [Authorize(Roles = "Customer,Admin")]
    [HttpPost("create-operation/{proposalId}")]
    public async Task<IActionResult> CreateOperation(int proposalId)
    {
        var proposal = await _proposalRepo.GetProposal(proposalId);
        var toolsInProposal = await _proposalRepo.GetProposedTools(proposalId);
        // return Ok(toolsInProposal);
        // foreach()
        Operation operationForCreate = new Operation
        {
            WorkerId = proposal.WorkerId,
            CustomerId = proposal.Order.OrderId,
            StartDate = System.DateTime.Now,
            Description = proposal.Order.Description
        };
        _appRepo.Add(operationForCreate);
        
        
        
        await _appRepo.SaveAll();
        foreach(ToolInProposal tip in toolsInProposal)
        {
            // operationForCreate.ToolsInRent.Add(tip.Tool);
            tip.Tool.OperationId = operationForCreate.OperationId;
            _appRepo.Update(tip.Tool);
        }
        await _appRepo.SaveAll();
        return Ok(operationForCreate);
        // var i = proposal.ProposedTools.GetEnumerator();
        // while (i.j < proposal.ProposedTools.Count)
        // {
        //     i.MoveNext();
        //     ToolInProposal tip = new ToolInProposal
        //     {
        //         ProposalId = proposalForCreate.ProposalId,
        //         ToolId = i.Current.ToolId
        //     };
        //     _appRepo.Add(tip);
        //     j++;
        // }
        // i.MoveNext();
        // foreach (ToolInProposal t in proposal.ProposedTools)

        //     Operation operationForCreate = new Operation
        //     {

        //     };
        // Proposal proposalForCreate = new Proposal
        // {
        //     OrderId = proposalDto.OrderId,
        //     WorkerId = proposalDto.WorkerId
        // };
        // _appRepo.Add(proposalForCreate);
        // await _appRepo.SaveAll();
        // int j = 0;
        // var i = proposalDto.ToolsInProposal.GetEnumerator();
        // while (j < proposalDto.ToolsInProposal.Count)
        // {
        //     i.MoveNext();
        //     ToolInProposal tip = new ToolInProposal
        //     {
        //         ProposalId = proposalForCreate.ProposalId,
        //         ToolId = i.Current.ToolId
        //     };
        //     _appRepo.Add(tip);
        //     j++;
        // }
        // i.MoveNext();
        // if (await _appRepo.SaveAll())
        // {
        //     return Ok("Proposal added");
        //     // return Ok(i.Current);
        // }
        // return BadRequest("Problem adding proposal");
    }

}
}