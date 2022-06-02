using Kdz.Gateway.Models;

namespace Kdz.Gateway.Persistence;

public interface IUserAccessRepository
{
    Task<IEnumerable<string>> GetAccessUrlsByRoleAsync(Role userRole);
}