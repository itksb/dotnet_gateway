using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kdz.Gateway.Pages;

[Authorize]
public class PrivacyModel : PageModel
{
    public string? UserName { get; set; }

    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var user = HttpContext.User.Identity;
        if (user is not null && user.IsAuthenticated)
        {
            UserName = user.Name;
        }
    }
}