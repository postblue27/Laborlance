using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<User>> GetUsers();
        Task<List<User>> GetUsersByRole(string roleName);
    }
}