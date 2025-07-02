using Microsoft.AspNetCore.Mvc.Filters;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Authentication, filter ve global uygulanan kesişen işler ortak geliştirilmiştir.
// Bu filter, MVC Controller ve Razor Page isteklerini konsola loglar.

public class LogActivityFilter : IActionFilter, IPageFilter
{
    // MVC Controller action'lar çalıştırılmadan önce tetiklenir
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var path = context.HttpContext.Request.Path;
        var query = context.HttpContext.Request.QueryString;
        Console.WriteLine($"[LogActivity] MVC Controller Request to: {path}{query}");
    }

    // MVC Controller action çalıştıktan sonra tetiklenir (biz burada boş bıraktık)
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // no-op
    }

    // Razor Page handler çalıştırılmadan önce tetiklenir
    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var path = context.HttpContext.Request.Path;
        var query = context.HttpContext.Request.QueryString;
        Console.WriteLine($"[LogActivity] Razor Page Request to: {path}{query}");
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
}
