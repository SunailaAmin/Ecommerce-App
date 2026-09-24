using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);

    Task<string> LoginAsync(LoginDto dto);

    Task<string> RegisterAdminAsync(AdminRegisterDto dto);
}