using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IToolRepository
    {
        Task<List<Tool>> GetRenterTools(int RenterId);
        Task<Tool> GetToolById(int toolId);
        Task<List<Tool>> GetAllTools();
        Task<ToolImage> GetToolImageByPublicIdAsync(string publicId);
    }
}