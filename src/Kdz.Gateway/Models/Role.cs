namespace Kdz.Gateway.Models;

public class Role
{
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Argument name is null or empty");
            }

            _name = value;
        }
    }

    public Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Argument name is null or empty");
        }

        _name = string.Empty;
        Name = name;
    }
}