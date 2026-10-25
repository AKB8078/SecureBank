namespace WebApplication1.DTOs
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; } 
        public string Email { get; set; } 

        public string PhoneNumber { get; set; } 

        public string UserStatus { get; set; } 

        public DateTime CreatedAtUtc { get; set; }
    }
}
