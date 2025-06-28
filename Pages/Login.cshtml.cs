using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    private readonly IUserContextService _userContext;

    [BindProperty]
    public string Email { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public string ErrorMessage { get; set; } = "";

    public LoginModel(IUserContextService userContext)
    {
        _userContext = userContext;
    }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (_userContext.TryLogin(Email, Password))
        {
            return RedirectToPage("/Index");
        }

        ErrorMessage = "Invalid email or password.";
        return Page();
    }
}
