using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laborlance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IAppRepository _appRepo;
        public OrderController(IOrderRepository orderRepo, IAppRepository appRepo)
        {
            _appRepo = appRepo;
            _orderRepo = orderRepo;

        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(Order orderToCreate)
        {
            _appRepo.Add(orderToCreate);
            if(await _appRepo.SaveAll())
                return Ok();
            return BadRequest("Problem creating order");
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("get-customer-orders/{CustomerId}")]
        public async Task<IActionResult> GetCustomerOrders(int CustomerId)
        {
            var customerOrders = await _orderRepo.GetCustomerOrders(CustomerId);
            return Ok(customerOrders);
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("delete-order/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var order = await _orderRepo.GetOrderById(orderId);
            _appRepo.Delete(order);
            if (await _appRepo.SaveAll())
            {
                return Ok(order);
            }
            return BadRequest("Problem deleting order");
        }

        [Authorize(Roles = "Customer,Admin,Worker")]
        [HttpGet("get-order-by-id/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderRepo.GetOrderById(orderId);
            return Ok(order);
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("get-order-proposals-with-tools/{orderId}")]
        public async Task<IActionResult> GetOrderProposalsWithTools(int orderId)
        {
            var proposals = await _orderRepo.GetProposalTools(orderId);
            return Ok(proposals);
        }

        [Authorize(Roles = "Worker,Admin")]
        [HttpGet("get-orders-by-search-string/{searchString}")]
        public async Task<IActionResult> GetOrdersBySearchString(string searchString)
        {
            var orders = await _orderRepo.GetOrdersBySearchString(searchString);
            return Ok(orders);
        }
        [Authorize(Roles = "Worker,Admin")]
        [HttpGet("get-all-orders")]
        public async Task<IActionResult> GetAllOrders(string searchString)
        {
            var orders = await _orderRepo.GetAllOrders();
            return Ok(orders);
        }
    }
}