using System.Text.Json.Serialization;

namespace Kdz.Gateway.Persistence;

public class UserRolesOptions // RoleUserAccess 
{
    public const string Position = "RoleUserAccess";

    [JsonPropertyName("roles")]
    [ConfigurationKeyName("roles")]
    public RoleOption[] Roles { get; set; } = Array.Empty<RoleOption>();
}

public class RoleOption
{
    [JsonPropertyName("name")]
    [ConfigurationKeyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("users")]
    [ConfigurationKeyName("users")]
    public UserOption[] Users { get; set; } = Array.Empty<UserOption>();

    [JsonPropertyName("access_urls")]
    [ConfigurationKeyName("access_urls")]
    public string[] AccessUrls { get; set; } = Array.Empty<string>();
}

public class UserOption
{
    [JsonPropertyName("name")]
    [ConfigurationKeyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    [ConfigurationKeyName("password")]
    public string Password { get; set; } = string.Empty;
}