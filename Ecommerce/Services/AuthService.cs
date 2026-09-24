using Ecommerce.Domain;
using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace Ecommerce.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new Exception("Email already exists");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = "Customer"
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(user, dto.Password);

        await _userRepository.CreateAsync(user);

        return "User registered successfully";
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("Invalid Email or Password");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid Email or Password");

        return GenerateJwtToken(user);
    }

    public async Task<string> RegisterAdminAsync(
    AdminRegisterDto dto)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new Exception("Email already exists");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = "Admin"
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                dto.Password);

        await _userRepository.CreateAsync(user);

        return "Admin registered successfully";
    }
    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}