using Microsoft.AspNetCore.Mvc.Filters;

public class LogActivityFilter : IActionFilter, IPageFilter
{
    // MVC Controller action'lar için
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var path = context.HttpContext.Request.Path;
        var query = context.HttpContext.Request.QueryString;
        Console.WriteLine($"[LogActivity] MVC Controller Request to: {path}{query}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // no-op
    }

    // Razor Page handler'lar için
    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var path = context.HttpContext.Request.Path;
        var query = context.HttpContext.Request.QueryString;
        Console.WriteLine($"[LogActivity] Razor Page Request to: {path}{query}");
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
}
