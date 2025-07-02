using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Bu sayfa giriş (login) işlemini yapan Razor Page’in backend kodudur.
// Authentication kısmı ortak geliştirildiği için bunu hepimiz yaptık.

public class LoginModel : PageModel
{
    private readonly IUserContextService _userContext;
    // Kullanıcı oturum servisi (email, password kontrolü burada yapılır)

    [BindProperty]
    public string Email { get; set; } = "";
    // Formdan gelen email input'u

    [BindProperty]
    public string Password { get; set; } = "";
    // Formdan gelen şifre input'u

    public string ErrorMessage { get; set; } = "";
    // Giriş başarısızsa gösterilecek hata mesajı

    public LoginModel(IUserContextService userContext)
    {
        _userContext = userContext;
    }

    public void OnGet() { }
    // Sayfa GET ile açıldığında herhangi bir işlem yapmaz

    public IActionResult OnPost()
    {
        // Kullanıcı girişi kontrol edilir
        if (_userContext.TryLogin(Email, Password))
        {
            return RedirectToPage("/Index"); // Başarılı login sonrası anasayfaya yönlen
        }

        ErrorMessage = "Invalid email or password."; // Hatalı giriş
        return Page(); // Sayfayı tekrar yükle (hata mesajı gözükecek)
    }
}
