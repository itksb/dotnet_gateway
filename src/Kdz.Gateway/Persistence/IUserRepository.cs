using Kdz.Gateway.Models;

namespace Kdz.Gateway.Persistence;

public interface IUserRepository
{
    Task<KdzUser?> GetByLoginAndPasswordAsync(string login, string password);

    Task<IEnumerable<KdzUser>> ListAllAsync();
}