namespace Kdz.Gateway.Models;

public class KdzUser
{
    private string _userName;
    private string _password;
    private Role _role;


    public KdzUser(string userName, string password, Role role)
    {
        _userName = userName ?? throw new ArgumentNullException(nameof(userName));
        _password = password ?? throw new ArgumentNullException(nameof(password));
        _role = role ?? throw new ArgumentNullException(nameof(role));
    }

    public string UserName
    {
        get => _userName;

        set
        {
            if (value == null)
            {
                throw new ArgumentException("Null value");
            }

            if (value.Length < 5)
            {
                throw new ArgumentException("UserName должно быть не менее 5 символов");
            }

            _userName = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (value == null)
            {
                throw new ArgumentException("Null value");
            }

            if (value.Length < 5)
            {
                throw new ArgumentException("Password должty быть не менее 5 символов");
            }

            _password = value;
        }
    }

    public Role Role
    {
        get => _role;
        set => _role = value ?? throw new ArgumentNullException(nameof(_role));
    }
}