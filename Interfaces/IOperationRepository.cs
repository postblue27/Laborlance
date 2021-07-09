using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IOperationRepository
    {
        Task<List<Operation>> GetCustomerOperations(int customerId);
        Task<List<Operation>> GetWorkerOperations(int workerId);
        Task<Operation> GetOperationById(int operationId);
    }
}