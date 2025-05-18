using Microsoft.AspNetCore.Mvc;
using ShiftManagementSystem.Application.DTOs.Auth;
using ShiftManagementSystem.Application.Services.Auth;

namespace ShiftManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var result = await _authService.RegisterAsync(model);
        
        if (!result.Success)
            return BadRequest(new { message = result.Message });
            
        return Ok(new { message = result.Message });
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var result = await _authService.LoginAsync(model);
        
        if (!result.Success)
            return BadRequest(new { message = result.Message });
            
        return Ok(new { token = result.Token, message = result.Message });
    }
} 