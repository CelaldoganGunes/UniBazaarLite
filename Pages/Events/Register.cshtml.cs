using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Yapımcı: Burak Kılıç
// Subsystem A - Razor Pages kısmı
// Bu sayfa, bir etkinliğe kayıt olmak için Register Razor Page’in backend kodudur.

namespace UniBazaarLite.Pages.Events
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository<Event> _eventRepo; // Etkinlikleri almak için repository

        public RegisterModel(IRepository<Event> eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [BindProperty]
        public string AttendeeName { get; set; } = "";
        // Formdan post ile gelen katılımcı ismini model binding yapar

        public Event? SelectedEvent { get; set; }
        // Kayıt yapılacak etkinlik detayını tutar

        public IActionResult OnGet(int id)
        {
            // Sayfa ilk açıldığında id ile etkinliği getir
            SelectedEvent = _eventRepo.GetById(id);
            if (SelectedEvent == null)
            {
                return NotFound(); // id geçersizse 404 döner
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            // İsim boşsa modelstate'e hata ekle
            if (string.IsNullOrWhiteSpace(AttendeeName))
            {
                ModelState.AddModelError("AttendeeName", "Name is required.");
            }

            // id ile etkinliği tekrar çek
            SelectedEvent = _eventRepo.GetById(id);
            if (SelectedEvent == null)
            {
                return NotFound(); // etkinlik yoksa 404
            }

            if (!ModelState.IsValid)
            {
                return Page(); // Hatalıysa aynı sayfayı tekrar göster
            }

            TempData["Message"] = $"Thanks {AttendeeName} for registering to {SelectedEvent.Title}!";
            return RedirectToPage("Index"); // Başarılı kayıt sonrası Index’e dön
        }
    }
}
