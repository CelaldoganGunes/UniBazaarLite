using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
    private readonly IUserContextService _userContext;

    public LogoutModel(IUserContextService userContext)
    {
        _userContext = userContext;
    }

    public IActionResult OnGet()
    {
        _userContext.Logout();
        return RedirectToPage("/Index");
    }
}
