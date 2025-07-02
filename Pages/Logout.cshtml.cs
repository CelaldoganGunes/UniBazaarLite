using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Bu sayfa logout (çıkış yap) işlemini yapan Razor Page’in backend kodudur.
// Authentication altyapısını ortak geliştirdiğimiz için bunu hepimiz yaptık.

public class LogoutModel : PageModel
{
    private readonly IUserContextService _userContext;
    // Kullanıcının oturumunu yönetmek için servis (login / logout)

    public LogoutModel(IUserContextService userContext)
    {
        _userContext = userContext;
    }

    public IActionResult OnGet()
    {
        _userContext.Logout(); // Çıkış yap
        return RedirectToPage("/Index"); // Anasayfaya yönlendir
    }
}
