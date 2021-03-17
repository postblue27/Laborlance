namespace Laborlance_API.Models
{
    public class Tool
    {
        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public double RentalPrice { get; set; }
        public int RenterId { get; set; }
        public Renter Renter { get; set; }
        public int? OperationId { get; set; }
        public Operation Operation { get; set; }
    }
}