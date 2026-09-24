using Ecommerce.Domain;

namespace Ecommerce.Services.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User> CreateAsync(User user);

}