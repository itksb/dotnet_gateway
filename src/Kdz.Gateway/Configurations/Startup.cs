using Kdz.Gateway.Persistence;

namespace Kdz.Proxy.Configurations;

internal static class Startup
{
    private const string ConfigurationDirectory = "Configurations";
    private static readonly string[] ConfigurationNames;

    static Startup()
    {
        ConfigurationNames = new[]
        {
            "logging",
            "yarp-proxy",
            "cookie-authentication-options",
            "session",
            "role-user-access"
        };
    }

    /// <summary>
    /// Adds configuration file paths to App Configuration
    /// </summary>
    /// <param name="configureHostBuilder"></param>
    /// <returns></returns>
    internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder configureHostBuilder)
    {
        configureHostBuilder.ConfigureAppConfiguration((context, config) =>
        {
            IHostEnvironment env = context.HostingEnvironment;

            config.AddJsonFile("appsettings.json", false, true);
            config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true);

            Array.ForEach(
                GenerateConfigFilePaths(
                    ConfigurationNames,
                    ConfigurationDirectory),
                configurationFilePath => config.AddJsonFile(configurationFilePath, false, true));

            Array.ForEach(
                GenerateOptionalConfigFilePaths(
                    ConfigurationNames,
                    ConfigurationDirectory,
                    context.HostingEnvironment.EnvironmentName),
                configurationFilePath => config.AddJsonFile(configurationFilePath, true, true));

            config.AddEnvironmentVariables();
        });

        return configureHostBuilder;
    }


    private static string[] GenerateConfigFilePaths(string[] configFileNames, string configDir)
    {
        var result = new string[configFileNames.Length];
        for (int i = 0; i < configFileNames.Length; i++)
        {
            result[i] = Path.Combine(configDir, $"{configFileNames[i]}.json");
        }

        return result;
    }

    private static string[] GenerateOptionalConfigFilePaths(string[] configFileNames, string configDir,
        string environmentName)
    {
        var result = new string[configFileNames.Length];
        for (int i = 0; i < configFileNames.Length; i++)
        {
            result[i] = Path.Combine(configDir, $"{configFileNames[i]}.{environmentName}.json");
        }

        return result;
    }


    internal static IServiceCollection ConfigureUserOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<UserRolesOptions>(
            config.GetSection(UserRolesOptions.Position)
        );

        return services;
    }
}