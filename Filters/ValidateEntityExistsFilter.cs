using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Bu filtre, üçümüzün ortak geliştirdiği bir cross-cutting concern.
// MVC action'larda id parametresiyle istenen entity gerçekten var mı diye kontrol eder.
// Yoksa kullanıcıyı anasayfaya (/Index) yönlendirir.

public class ValidateEntityExistsFilter<T> : IActionFilter where T : class
{
    private readonly IRepository<T> _repository;

    public ValidateEntityExistsFilter(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Eğer action parametrelerinde "id" varsa kontrol et
        if (context.ActionArguments.ContainsKey("id"))
        {
            var id = (int)context.ActionArguments["id"];
            var entity = _repository.GetById(id);

            // Eğer entity bulunamazsa kullanıcıyı Index sayfasına redirect et
            if (entity == null)
            {
                context.Result = new RedirectToPageResult("/Index");
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // İşlem sonrası yapılacak bir şey yok (no-op)
    }
}
