using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetCustomerOrders(int CustomerId);
        Task<Order> GetOrderById(int orderId);
        Task<List<Proposal>> GetProposalTools(int orderId);
        Task<List<Order>> GetOrdersBySearchString(string searchString);
        Task<List<Order>> GetAllOrders() ;
    }
}