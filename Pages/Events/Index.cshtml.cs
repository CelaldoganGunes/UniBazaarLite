using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;


namespace UniBazaarLite.Pages.Events // ← BU SATIR ÖNEMLİ
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IConfiguration _config;

        public List<Event> Events { get; set; } = new();
        public string? SiteTitle { get; set; }

        public IndexModel(IRepository<Event> eventRepo, IConfiguration config)
        {
            _eventRepo = eventRepo;
            _config = config;
        }

        public void OnGet()
        {
            Events = _eventRepo.GetAll().ToList();
            SiteTitle = _config["SiteSettings:SiteTitle"];
        }
    }
}
