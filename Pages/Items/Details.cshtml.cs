using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Yapımcı: Hüseyin Kaplan
// Subsystem B - Razor Pages kısmı
// Bu sayfa, tek bir ilanın (classified item) detayını gösteren Details Razor Page’in backend kodudur.

namespace UniBazaarLite.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<ClassifiedItem> _itemRepo; // İlanları almak için repository

        public ClassifiedItem? Item { get; set; }
        // View tarafında gösterilecek seçili ilan

        public DetailsModel(IRepository<ClassifiedItem> itemRepo)
        {
            _itemRepo = itemRepo;
        }

        public IActionResult OnGet(int id)
        {
            // id ile ilanı getir
            Item = _itemRepo.GetById(id);
            if (Item == null)
            {
                // İlan bulunamazsa kullanıcıyı index’e yönlendir, bir de TempData ile uyarı mesajı ver
                TempData["Message"] = "Item not found. Redirected to classifieds.";
                return RedirectToPage("/Items/Index");
            }

            return Page(); // Her şey tamamsa sayfayı render et
        }
    }
}
