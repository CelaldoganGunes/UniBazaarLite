public interface IUserContextService
{
    bool IsAdmin { get; }
    bool IsStudent { get; }
    string? Email { get; }

    bool TryLogin(string email, string password);
    void LoginAsAdmin();
    void LoginAsStudent();
}
