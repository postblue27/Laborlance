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
        [Authorize(Roles = "Customer")]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(Order orderToCreate)
        {
            _appRepo.Add(orderToCreate);
            if(await _appRepo.SaveAll())
                return Ok("Order succsessfully created");
            return BadRequest("Problem creating order");
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("get-customer-orders/{CustomerId}")]
        public async Task<IActionResult> GetCustomerOrders(int CustomerId)
        {
            var customerOrders = await _orderRepo.GetCustomerOrders(CustomerId);
            return Ok(customerOrders);
        }
    }
}