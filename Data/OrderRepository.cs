using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Order>> GetCustomerOrders(int customerId)
        {
            var cutomerOrders = await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
            return cutomerOrders;
        }
    }
}