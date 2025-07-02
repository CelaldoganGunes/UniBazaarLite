using Microsoft.AspNetCore.Mvc.RazorPages;

// Yapımcı: Hüseyin Kaplan
// Subsystem B - Razor Pages kısmı
// Bu sayfa, tüm ilanları listeleyen Index Razor Page’in backend kodudur.

namespace UniBazaarLite.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<ClassifiedItem> _itemRepo; 
        // Bellek içi repository ile ilanları çeker

        public List<ClassifiedItem> Items { get; set; } = new();
        // View’da kullanılacak ilan listesi

        public IndexModel(IRepository<ClassifiedItem> itemRepo)
        {
            _itemRepo = itemRepo;
        }

        public void OnGet()
        {
            // Sayfa GET isteği aldığında tüm ilanları getir
            Items = _itemRepo.GetAll().ToList();
        }
    }
}
