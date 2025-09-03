using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Identity.Data;


namespace Backend.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("api/auth/token")]
    public IActionResult GenerateToken([FromBody] LoginRequest request)
    {
        // In a real application, you would validate the user's credentials here.
        // For demonstration purposes, we'll assume the user is valid and has userId = 1.
        int userId = 1; // This should come from your user validation logic
        string token = _authService.GenerateJwtToken(userId, request.Email);
        return Ok(new { Token = token });
    }
}   