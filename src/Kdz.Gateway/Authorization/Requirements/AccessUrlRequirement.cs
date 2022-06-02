using Microsoft.AspNetCore.Authorization;

namespace Kdz.Gateway.Authorization.Requirements;

public class AccessUrlRequirement : IAuthorizationRequirement
{
    public AccessUrlRequirement()
    {
    }
}