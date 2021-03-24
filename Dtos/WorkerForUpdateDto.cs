namespace Laborlance_API.Dtos
{
    public class WorkerForUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public double HourlyWage { get; set; }
        public double Rating { get; set; }
    }
}