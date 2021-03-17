namespace Laborlance_API.Models
{
    public class ToolInRent
    {
        public int ToolInRentId { get; set; }
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
        public int OperationId { get; set; }
        public Operation Operation { get; set; }
        public int DaysInRent { get; set; }
    }
}