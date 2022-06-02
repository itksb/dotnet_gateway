using System.Security.Claims;
using Kdz.Gateway.Models;
using Kdz.Gateway.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kdz.Gateway.Pages;

public class LoginModel : PageModel
{
    [BindProperty] public Credential Credential { get; set; } = new Credential();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IUserRepository userRepository, string? returnUrl)
    {
        if (!ModelState.IsValid) return Page();

        // аутентификация здесь...
        KdzUser? user = await userRepository
            .GetByLoginAndPasswordAsync(Credential.UserName, Credential.Password);

        if (user == null)
        {
            ModelState.AddModelError(
                "Authentication",
                "Аутентификация не прошла. Такой пары логин:пароль не существует.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // security context
        var claimsPrincipal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        else
            return Redirect("/");
    }
}