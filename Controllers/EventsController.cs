using Microsoft.AspNetCore.Mvc;

public class EventsController : Controller
{
    private readonly IRepository<Event> _eventRepo;
    private readonly IUserContextService _userContext;

    public EventsController(IRepository<Event> eventRepo, IUserContextService userContext)
    {
        _eventRepo = eventRepo;
        _userContext = userContext;
    }

    [HttpGet("/events/create")]
    public IActionResult Create()
    {

        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        var newEvent = new Event
        {
            Title = "Başlık girin.",
            Description = " Açıklama girin.",
            Date = DateTime.Today
        };
        return View(newEvent);
    }

    [HttpPost("/events/create")]
    public IActionResult Create(Event model)
    {
        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _eventRepo.Add(model);
        TempData["Message"] = $"Event '{model.Title}' created.";
        return RedirectToPage("/Events/Index");
    }


    [HttpGet("/events/edit/{id}")]
    [ServiceFilter(typeof(ValidateEntityExistsFilter<Event>))]
    public IActionResult Edit(int id)
    {
        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        var ev = _eventRepo.GetById(id);
        return View(ev);
    }

    [HttpPost("/events/edit/{id}")]
    [ServiceFilter(typeof(ValidateEntityExistsFilter<Event>))]
    public IActionResult Edit(int id, Event model)
    {
        if (!_userContext.IsAdmin)
        {
            return RedirectToPage("/Login");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _eventRepo.Update(model);
        TempData["Message"] = $"Event '{model.Title}' updated.";
        return RedirectToPage("/Events/Index");
    }
}
