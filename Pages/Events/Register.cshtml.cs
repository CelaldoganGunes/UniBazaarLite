using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniBazaarLite.Pages.Events
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository<Event> _eventRepo;

        public RegisterModel(IRepository<Event> eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [BindProperty]
        public string AttendeeName { get; set; } = "";

        public Event? SelectedEvent { get; set; }

        public IActionResult OnGet(int id)
        {
            SelectedEvent = _eventRepo.GetById(id);
            if (SelectedEvent == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (string.IsNullOrWhiteSpace(AttendeeName))
            {
                ModelState.AddModelError("AttendeeName", "Name is required.");
            }

            SelectedEvent = _eventRepo.GetById(id);
            if (SelectedEvent == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            TempData["Message"] = $"Thanks {AttendeeName} for registering to {SelectedEvent.Title}!";
            return RedirectToPage("Index");
        }
    }
}
