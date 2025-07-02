using Microsoft.Extensions.Configuration;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Authentication modülü
// Bu sınıf sahte (fake) bir kullanıcı oturumu servisidir.
// appsettings.json'dan admin ve student kullanıcı bilgilerini okur
// Basit giriş/çıkış kontrolü sağlar.

public class FakeUserContextService : IUserContextService
{
    private readonly string _adminEmail;
    private readonly string _adminPassword;
    private readonly string _studentEmail;
    private readonly string _studentPassword;

    private bool _isAdmin;
    private bool _isStudent;
    private string? _email;

    // Kullanıcı rol ve email bilgilerini dışarıya sunan property'ler
    public bool IsAdmin => _isAdmin;
    public bool IsStudent => _isStudent;
    public string? Email => _email;

    public FakeUserContextService(IConfiguration config)
    {
        // user secrets'tan admin/student bilgilerini oku
        _adminEmail = config["Admin:Email"] ?? "admin";
        _adminPassword = config["Admin:Password"] ?? "123";
        _studentEmail = config["Student:Email"] ?? "student";
        _studentPassword = config["Student:Password"] ?? "asd";
    }

    public bool TryLogin(string email, string password)
    {
        // Eğer admin bilgileri doğru girilmişse admin olarak giriş yap
        if (email == _adminEmail && password == _adminPassword)
        {
            LoginAsAdmin();
            return true;
        }
        // Eğer student bilgileri doğru girilmişse student olarak giriş yap
        if (email == _studentEmail && password == _studentPassword)
        {
            LoginAsStudent();
            return true;
        }
        // Giriş başarısız
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
        // Çıkış yapınca tüm rol ve email bilgileri temizlenir
        _isAdmin = false;
        _isStudent = false;
        _email = null;
    }
}
