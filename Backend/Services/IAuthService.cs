

namespace Backend.Services;

public interface IAuthService
{
    string GenerateJwtToken(int userId, string username);
}