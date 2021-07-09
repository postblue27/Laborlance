using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DataContext _context;
        public OperationRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Operation>> GetCustomerOperations(int customerId)
        {
            var cutomerOperations = await _context.Operations.Where(o => o.CustomerId == customerId)
                .Include(o => o.ToolsInRent).Include(o => o.Worker).ToListAsync();
            return cutomerOperations;
        }
        public async Task<List<Operation>> GetWorkerOperations(int workerId)
        {
            var cutomerOperations = await _context.Operations.Where(o => o.WorkerId == workerId)
                .Include(o => o.ToolsInRent).Include(o => o.Worker).Include(o => o.Customer).ToListAsync();
            return cutomerOperations;
        }
         public async Task<Operation> GetOperationById(int operationId)
        {
            var operation = await _context.Operations.Include(o => o.ToolsInRent).Where(o => o.OperationId == operationId).FirstOrDefaultAsync();
            return operation;
        }
    }
}