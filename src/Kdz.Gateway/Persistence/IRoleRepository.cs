using Kdz.Gateway.Models;

namespace Kdz.Gateway.Persistence;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
}