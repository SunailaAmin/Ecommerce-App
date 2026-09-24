using Ecommerce.Domain;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Ecommerce.Services.Interfaces;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.UnitTests;

public class AuthServiceTests
{
    [Fact]
    public async Task Register_ShouldCreateUser()
    {
        var repoMock =
            new Mock<IUserRepository>();

        repoMock
            .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var config =
            new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    { "Jwt:Key", "VeryLongSecretKeyForTesting123456" },
                    { "Jwt:Issuer", "TestIssuer" },
                    { "Jwt:Audience", "TestAudience" }
                })
            .Build();

        var service =
            new AuthService(
                repoMock.Object,
                config);

        var result =
            await service.RegisterAsync(
                new RegisterDto
                {
                    Name = "Sunaila",
                    Email = "test@mail.com",
                    Password = "Password123"
                });

        result.Should()
            .Be("User registered successfully");
    }
}