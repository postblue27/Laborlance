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
            var cutomerOrders = await _context.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Proposals).ToListAsync();
            return cutomerOrders;
        }
        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await _context.Orders.Include(o => o.Proposals).FirstOrDefaultAsync(o => o.OrderId == orderId);
            return order;
        }
        public async Task<List<Proposal>> GetProposalTools(int orderId)
        {
            // var proposalsFromDb = 
            //     from p in _context.Proposals
            //     join tip in _context.ToolsInProposals on p.ProposalId equals tip.ProposalId
            //     where p.OrderId == orderId
            //     select p;
            // var proposalsList = await proposalsFromDb.ToListAsync();

            // var proposalsList = await _context.Proposals.Include(p => p.ProposedTools.Select(pt => pt.Tool))
            //     .Where(p => p.OrderId == orderId).ToListAsync();

            var proposalsList = await _context.Proposals.Include("ProposedTools.Tool").Include(p => p.Worker)
                .Where(p => p.OrderId == orderId).ToListAsync();
            return proposalsList;
        }

        public async Task<List<Order>> GetOrdersBySearchString(string searchString)
        {
            var orders = await _context.Orders.Where(o => o.Description.Contains(searchString)).ToListAsync();
            return orders;
        }
        public async Task<List<Order>> GetAllOrders() 
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }
    }
}