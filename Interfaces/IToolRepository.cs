using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IToolRepository
    {
        Task<List<Tool>> GetRenterTools(int RenterId);
    }
}