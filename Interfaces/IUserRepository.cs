using System.Threading.Tasks;
using Laborlance_API.Dtos;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IUserRepository
    {
        Task<Worker> UpdateWorker(WorkerForUpdateDto worker);
    }
}