using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetCustomerOrders(int CustomerId);
    }
}