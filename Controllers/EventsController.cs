using Microsoft.AspNetCore.Mvc;

public class EventsController : Controller
{
    private readonly IRepository<Event> _eventRepo;

    public EventsController(IRepository<Event> eventRepo)
    {
        _eventRepo = eventRepo;
    }

    [HttpGet("/events/create")]
    public IActionResult Create()
    {
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
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _eventRepo.Add(model);
        TempData["Message"] = $"Event '{model.Title}' created.";
        return RedirectToPage("/Events/Index");
    }

    [HttpGet("/events/edit/{id}")]
    public IActionResult Edit(int id)
    {
        var ev = _eventRepo.GetById(id);
        if (ev == null) return NotFound();

        return View(ev);
    }

    [HttpPost("/events/edit/{id}")]
    public IActionResult Edit(int id, Event model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _eventRepo.Update(model);
        TempData["Message"] = $"Event '{model.Title}' updated.";
        return RedirectToPage("/Events/Index");
    }
}
