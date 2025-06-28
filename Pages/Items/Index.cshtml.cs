using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniBazaarLite.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<ClassifiedItem> _itemRepo;

        public List<ClassifiedItem> Items { get; set; } = new();

        public IndexModel(IRepository<ClassifiedItem> itemRepo)
        {
            _itemRepo = itemRepo;
        }

        public void OnGet()
        {
            Items = _itemRepo.GetAll().ToList();
        }
    }
}
