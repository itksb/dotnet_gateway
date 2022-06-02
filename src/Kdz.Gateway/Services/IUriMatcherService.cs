namespace Kdz.Gateway.Services;

public interface IUriMatcherService
{
    bool Match(string routeTemplate, string requestPath);
}