using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using Kdz.Gateway.Authorization.Requirements;
using Kdz.Gateway.Models;
using Kdz.Gateway.Persistence;
using Kdz.Gateway.Services;
using Microsoft.AspNetCore.Authorization;

namespace Kdz.Gateway.Authorization.AuthorizationHandlers;

public class AccessUrlHandler : AuthorizationHandler<AccessUrlRequirement>
{
    private readonly IServiceProvider _provider;
    private readonly IUriMatcherService _uriMatcherService;


    public AccessUrlHandler(IServiceProvider provider, IUriMatcherService uriMatcherService)
    {
        _provider = provider;
        _uriMatcherService = uriMatcherService;
    }

    #region Overrides of AuthorizationHandler<TestRequirement>

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AccessUrlRequirement requirement
    )
    {
        IIdentity? user = context.User.Identity;

        if (user is not null && user.IsAuthenticated)
        {
            string roleName = context
                                  .User
                                  .FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value ??
                              throw new ArgumentException("Role claim is empty");

            IEnumerable<string> accessUrls;
            string requestPath = string.Empty;
            using (var scope = _provider.CreateScope())
            {
                IUserAccessRepository accessRepo = scope.ServiceProvider.GetService<IUserAccessRepository>() ??
                                                   throw new Exception(
                                                       "IUserAccessRepository is not available throw Dependency Injection");
                accessUrls = await accessRepo.GetAccessUrlsByRoleAsync(new Role(roleName));

                IHttpContextAccessor? httpContextAccessor = _provider.GetService<IHttpContextAccessor>();

                if (httpContextAccessor is null)
                {
                    throw new ArgumentNullException("IHttpContextAccessor",
                        "Use 'builder.Services.AddHttpContextAccessor();' in your configuration");
                }

                var httpContext = httpContextAccessor?.HttpContext;
                if (httpContext is null)
                {
                    throw new Exception("HttpContext is null");
                }

                requestPath = httpContext?.Request.Path.Value ?? requestPath;
            }
            
            if (!accessUrls.Any())
            {
                return; // no urls for the user
            }

            if (accessUrls.Any(template => _uriMatcherService.Match(template, requestPath)))
            {
                context.Succeed(requirement);
            }
        }
    }

    #endregion
}