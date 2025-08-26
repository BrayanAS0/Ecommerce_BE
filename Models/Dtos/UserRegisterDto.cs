namespace ApiEcommerce.Models.Dtos
{
    public class UserRegisterDto
    {
        public  string? ID { get; set; }
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }

    }
}
