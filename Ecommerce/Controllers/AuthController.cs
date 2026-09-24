using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        return Ok(new
        {
            Token = token
        });
    }

    [HttpPost("admin/register")]
    public async Task<IActionResult> RegisterAdmin(
    AdminRegisterDto dto)
    {
        var result =
            await _authService.RegisterAdminAsync(dto);

        return Ok(result);
    }
}