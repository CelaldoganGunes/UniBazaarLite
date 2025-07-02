using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

// Yapımcı: Burak Kılıç
// Subsystem A - Razor Pages kısmı
// Bu sayfa, etkinlikleri listeleyen Index Razor Page’in backend kodudur.
// Ayrıca site başlığını appsettings’ten çeker.

namespace UniBazaarLite.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Event> _eventRepo; // Event listesi için repository
        private readonly IConfiguration _config;        // appsettings.json okumak için config servisi

        public List<Event> Events { get; set; } = new(); // View’da kullanılacak event listesi
        public string? SiteTitle { get; set; }           // appsettings’ten çekilen site başlığı

        public IndexModel(IRepository<Event> eventRepo, IConfiguration config)
        {
            _eventRepo = eventRepo;
            _config = config;
        }

        public void OnGet()
        {
            Events = _eventRepo.GetAll().ToList();
            SiteTitle = _config["SiteSettings:SiteTitle"];
            // Böylece Index.cshtml'de @Model.SiteTitle ve @Model.Events çalışacak
        }
    }
}
