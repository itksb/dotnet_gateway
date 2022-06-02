using Microsoft.AspNetCore.Routing.Template;

namespace Kdz.Gateway.Services;

public class UriMatcherService : IUriMatcherService
{
    // Template docs: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-templates

    public bool Match(string routeTemplate, string requestPath)
    {
        if (routeTemplate is null || requestPath is null)
        {
            return false;
        }

        // The TemplateParser can only parse the route part, and not the query string.
        RouteTemplate template = TemplateParser.Parse(routeTemplate);
        TemplateMatcher matcher = new TemplateMatcher(template, GetDefaults(template));
        RouteValueDictionary values = new RouteValueDictionary();

        return matcher.TryMatch(requestPath, values);
    }


    private RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
    {
        var result = new RouteValueDictionary();

        foreach (var parameter in parsedTemplate.Parameters)
        {
            if (parameter.DefaultValue != null && parameter.Name != null)
            {
                result.Add(parameter.Name, parameter.DefaultValue);
            }
        }

        return result;
    }
}