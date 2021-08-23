namespace Laborlance_API.Models
{
    public class ToolImage
    {
        public int ToolImageId { get; set; }
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
    }
}