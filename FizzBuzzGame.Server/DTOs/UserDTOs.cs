namespace FizzBuzzGame.Server.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public ICollection<int> GameIds { get; set; }
    }
}
