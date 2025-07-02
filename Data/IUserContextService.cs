// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Authentication altyapısı ortak geliştirilmiştir.
// Bu interface, kullanıcı oturumu servisinin sözleşmesini belirler.
// Kimlik doğrulama (giriş/çıkış) ve rol kontrolü işlevlerini tanımlar.

public interface IUserContextService
{
    bool IsAdmin { get; }    // Kullanıcı admin mi?
    bool IsStudent { get; }  // Kullanıcı student mı?
    string? Email { get; }   // Kullanıcının email adresi

    bool TryLogin(string email, string password); // Email & şifre kontrol edip giriş yapar
    void LoginAsAdmin();     // Admin olarak login yap
    void LoginAsStudent();   // Student olarak login yap
    void Logout();           // Çıkış yap
}
