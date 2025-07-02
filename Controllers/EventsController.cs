using Microsoft.AspNetCore.Mvc;

// Yapımcı: Celaldoğan Güneş
// Subsystem A - MVC kısmı (EventsController)
// Bu controller, admin kullanıcıların etkinlik oluşturmasını ve düzenlemesini sağlar.
// Ayrıca TempData kullanarak işlem sonrası kullanıcıya mesaj döner.

public class EventsController : Controller
{
    private readonly IRepository<Event> _eventRepo; // Event CRUD işlemleri için repository
    private readonly IUserContextService _userContext; // Kullanıcının admin olup olmadığını kontrol etmek için

    public EventsController(IRepository<Event> eventRepo, IUserContextService userContext)
    {
        _eventRepo = eventRepo;
        _userContext = userContext;
    }

    [HttpGet("/events/create")]
    public IActionResult Create()
    {
        // Sadece admin kullanıcılar etkinlik oluşturabilir
        if (!_userContext.IsAdmin)
        {
            TempData["Message"] = "You must be an admin to create an event.";
            return RedirectToPage("/Events/Index");
        }

        // Varsayılan değerlerle yeni bir Event modeli oluşturuluyor
        var newEvent = new Event
        {
            Title = "Başlık girin.",
            Description = " Açıklama girin.",
            Date = DateTime.Today
        };
        return View(newEvent); // Create.cshtml'e model gönderiliyor
    }

    [HttpPost("/events/create")]
    public IActionResult Create(Event model)
    {
        // Admin kontrolü
        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        // Model doğrulaması başarısızsa aynı sayfa tekrar gösterilir
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Yeni event repository'e ekleniyor
        _eventRepo.Add(model);
        TempData["Message"] = $"Event '{model.Title}' created."; // TempData ile mesaj
        return RedirectToPage("/Events/Index"); // Razor Page listeleme sayfasına yönlendirme
    }

    [HttpGet("/events/edit/{id}")]
    [ServiceFilter(typeof(ValidateEntityExistsFilter<Event>))]
    public IActionResult Edit(int id)
    {
        // Admin kontrolü
        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        // Id ile event getiriliyor
        var ev = _eventRepo.GetById(id);
        return View(ev); // Edit.cshtml'e gönderiliyor
    }

    [HttpPost("/events/edit/{id}")]
    [ServiceFilter(typeof(ValidateEntityExistsFilter<Event>))]
    public IActionResult Edit(int id, Event model)
    {
        // Admin kontrolü
        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        // Model doğrulaması başarısızsa aynı sayfa tekrar gösterilir
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Event güncelleniyor
        _eventRepo.Update(model);
        TempData["Message"] = $"Event '{model.Title}' updated.";
        return RedirectToPage("/Events/Index"); // Razor Page listeleme sayfasına yönlendirme
    }
}
