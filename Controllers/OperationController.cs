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
            CustomerId = proposal.Order.CustomerId,
            StartDate = System.DateTime.Now,
            Description = proposal.Order.Description
        };
        _appRepo.Add(operationForCreate);
        
        // _appRepo.Delete(proposal.Order);
        await _appRepo.SaveAll();
        foreach(ToolInProposal tip in toolsInProposal)
        {
            // operationForCreate.ToolsInRent.Add(tip.Tool);
            tip.Tool.OperationId = operationForCreate.OperationId;
            _appRepo.Update(tip.Tool);
        }
        await _appRepo.SaveAll();
        return Ok(operationForCreate);
    }
    
    [Authorize(Roles = "Customer,Admin")]
    [HttpGet("get-customer-operations/{customerId}")]
    public async Task<IActionResult> GetCustomerOperations(int customerId)
    {
        var customerOperations = await _operationRepo.GetCustomerOperations(customerId);
        return Ok(customerOperations);
    }

    [Authorize(Roles = "Worker,Admin")]
    [HttpGet("get-worker-operations/{workerId}")]
    public async Task<IActionResult> GetWorkerOperations(int workerId)
    {
        var workerOperations = await _operationRepo.GetWorkerOperations(workerId);
        return Ok(workerOperations);
    }

    [Authorize(Roles = "Worker,Admin")]
    [HttpPost("close-operation/{operationId}/{finalCost}")]
    public async Task<IActionResult> CloseOperation(int operationId, float finalCost)
    {
        var operation = await _operationRepo.GetOperationById(operationId);
        operation.EndDate = System.DateTime.Now;
        operation.CurrentPrice = finalCost;
        _appRepo.Update(operation);
        foreach(var tool in operation.ToolsInRent)
        {
            tool.OperationId = null;
            _appRepo.Update(tool);
        }
        await _appRepo.SaveAll();
        return Ok(operation);
    }
}
}