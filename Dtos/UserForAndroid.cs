namespace Laborlance_API.Dtos
{
    public class UserForAndroid
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}