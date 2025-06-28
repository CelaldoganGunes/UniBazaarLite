using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidateEntityExistsFilter<T> : IActionFilter where T : class
{
    private readonly IRepository<T> _repository;

    public ValidateEntityExistsFilter(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.ContainsKey("id"))
        {
            var id = (int)context.ActionArguments["id"];
            var entity = _repository.GetById(id);

            if (entity == null)
            {
                context.Result = new RedirectToPageResult("/Index");
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // no-op
    }
}
