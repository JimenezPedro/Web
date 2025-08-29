namespace Backend.Models;

public class AuthResponseDto
{
    public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public UserResponseDto? User { get; set; }
}