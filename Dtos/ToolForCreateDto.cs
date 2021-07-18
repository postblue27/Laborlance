using Microsoft.AspNetCore.Http;

namespace Laborlance_API.Dtos
{
    public class ToolForCreateDto
    {
        public string ToolName { get; set; }
        public double RentalPrice { get; set; }
        public int RenterId { get; set; }
        public IFormFile[] Images { get; set; }
    }
}