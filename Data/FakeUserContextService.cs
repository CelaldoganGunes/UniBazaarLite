using Microsoft.Extensions.Configuration;

public class FakeUserContextService : IUserContextService
{
    private readonly string _adminEmail;
    private readonly string _adminPassword;
    private readonly string _studentEmail;
    private readonly string _studentPassword;

    private bool _isAdmin;
    private bool _isStudent;
    private string? _email;

    public bool IsAdmin => _isAdmin;
    public bool IsStudent => _isStudent;
    public string? Email => _email;

    public FakeUserContextService(IConfiguration config)
    {
        _adminEmail = config["Admin:Email"] ?? "admin";
        _adminPassword = config["Admin:Password"] ?? "123";
        _studentEmail = config["Student:Email"] ?? "student";
        _studentPassword = config["Student:Password"] ?? "asd";
    }

    public bool TryLogin(string email, string password)
    {
        if (email == _adminEmail && password == _adminPassword)
        {
            LoginAsAdmin();
            return true;
        }
        if (email == _studentEmail && password == _studentPassword)
        {
            LoginAsStudent();
            return true;
        }
        return false;
    }

    public void LoginAsAdmin()
    {
        _isAdmin = true;
        _isStudent = false;
        _email = _adminEmail;
    }

    public void LoginAsStudent()
    {
        _isAdmin = false;
        _isStudent = true;
        _email = _studentEmail;
    }

    public void Logout()
    {
        _isAdmin = false;
        _isStudent = false;
        _email = null;
    }
}
