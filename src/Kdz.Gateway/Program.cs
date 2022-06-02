using Kdz.Gateway.Authorization.AuthorizationHandlers;
using Kdz.Gateway.Authorization.Requirements;
using Kdz.Gateway.Persistence;
using Kdz.Gateway.Services;
using Kdz.Proxy.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfigurations();
builder.Services.ConfigureUserOptions(builder.Configuration);


// Add services to the container.
builder.Services.AddRazorPages()
    // See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0#tempdata
    .AddSessionStateTempDataProvider();

builder.Services.AddDistributedMemoryCache();
/*builder.Services.AddSession(options =>
{
    builder.Configuration.Bind("Session", options);
    options.IdleTimeout = TimeSpan.FromSeconds(20);
});*/

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "antikarapuz";
    options.FormFieldName = "antikarapuz";
    options.Cookie.HttpOnly = true;
    // the X-Frame-Options header will be generated for the response with value SAMEORIGIN.
    options.SuppressXFrameOptionsHeader = false;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => { builder.Configuration.Bind("CookieAuthenticationOptions", options); });


builder.Services.AddSingleton<IAuthorizationHandler, AccessUrlHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("accessUrlPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new AccessUrlRequirement());
    });
});

builder.Services.AddSingleton<ITimeService, SimpleTimeService>();
builder.Services.AddSingleton<IUriMatcherService, UriMatcherService>();

builder.Services.AddScoped<IUserRepository, UserJsonRepository>();
builder.Services.AddScoped<IRoleRepository, UserJsonRepository>();
builder.Services.AddScoped<IUserAccessRepository, UserJsonRepository>();


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddHttpContextAccessor();

var webApplication = builder.Build();

// Configure the HTTP request pipeline.
if (!webApplication.Environment.IsDevelopment())
{
    webApplication.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

// webApplication.UseCookiePolicy(); 

// app.UseHttpsRedirection();
webApplication.UseStaticFiles();

webApplication.UseRouting();

webApplication.UseSession();


webApplication.UseAuthentication();

webApplication.UseAuthorization();

webApplication.MapRazorPages();

webApplication.MapReverseProxy();

webApplication.Run();