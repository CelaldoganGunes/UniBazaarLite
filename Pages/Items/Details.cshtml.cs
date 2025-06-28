using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniBazaarLite.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<ClassifiedItem> _itemRepo;

        public ClassifiedItem? Item { get; set; }

        public DetailsModel(IRepository<ClassifiedItem> itemRepo)
        {
            _itemRepo = itemRepo;
        }

        public IActionResult OnGet(int id)
        {
            Item = _itemRepo.GetById(id);
            if (Item == null)
            {
                TempData["Message"] = "Item not found. Redirected to classifieds.";
                return RedirectToPage("/Items/Index");
            }

            return Page();
        }
    }
}
