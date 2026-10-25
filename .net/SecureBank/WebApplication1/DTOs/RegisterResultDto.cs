namespace WebApplication1.DTOs
{
    public class RegisterResultDto
    {
        public bool Succeeded { get; set; }

        public bool EmailAlreadyExists { get; set; }

        public string Message { get; set; } = string.Empty;

        public RegisterResponseDto? User { get; set; }

        public IDictionary<string, string[]>? ValidationErrors { get; set; }
    }
}
