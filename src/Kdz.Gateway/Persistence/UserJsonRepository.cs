using Kdz.Gateway.Models;
using Microsoft.Extensions.Options;

namespace Kdz.Gateway.Persistence;

public class UserJsonRepository : IUserRepository, IRoleRepository, IUserAccessRepository
{
    private readonly IOptionsMonitor<UserRolesOptions> _options;

    public UserJsonRepository(IOptionsMonitor<UserRolesOptions> options)
    {
        _options = options;
    }

    #region Implementation of IUserRepository

    public async Task<KdzUser?> GetByLoginAndPasswordAsync(string login, string password) =>
        (await ListAllAsync()).LastOrDefault(u => u.UserName == login && u.Password == password);


    public Task<IEnumerable<KdzUser>> ListAllAsync()
    {
        IEnumerable<KdzUser> users = _options.CurrentValue.Roles.SelectMany(role => role.Users,
                (role, user) => new KdzUser(user.Name, user.Password, new Role(role.Name)))
            .OrderBy(user => user.UserName, StringComparer.CurrentCultureIgnoreCase);

        return Task.FromResult(users);
    }

    #endregion


    #region Implementation of IRoleRepository

    public Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        RoleOption[] roles = _options.CurrentValue.Roles;
        Role[] result = new Role[roles.Length];

        for (int i = 0; i < roles.Length; i++)
        {
            RoleOption roleOption = roles[i];
            result[i] = new Role(roleOption.Name);
        }

        return Task.FromResult(result.AsEnumerable());
    }

    #endregion


    #region Implementation of IUserAccessRepository

    public Task<IEnumerable<string>> GetAccessUrlsByRoleAsync(Role userRole)
    {
        RoleOption[] roles = _options.CurrentValue.Roles;
        IEnumerable<string> urls = roles
            .Where(role => role.Name == userRole.Name)
            .SelectMany(
                roleOption => roleOption.AccessUrls,
                (roleOption, url) => url);
        return Task.FromResult(urls);
    }

    #endregion
}