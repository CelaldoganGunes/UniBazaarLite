using Microsoft.AspNetCore.Mvc;

// Yapımcı: Celaldoğan Güneş
// Subsystem B - MVC kısmı (ItemsController ve ItemsManagementController)
// Bu controllerlar classifieds (ilan) sistemi için API ve yönetim işlemlerini sağlar.

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<ClassifiedItem> _itemRepo;

    public ItemsController(IRepository<ClassifiedItem> itemRepo)
    {
        _itemRepo = itemRepo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var items = _itemRepo.GetAll();
        return Ok(items); // JSON olarak tüm ilanları döndürür
    }
}


public class ItemsManagementController : Controller
{
    private readonly IRepository<ClassifiedItem> _itemRepo;
    private readonly IUserContextService _userContext;

    public ItemsManagementController(IRepository<ClassifiedItem> itemRepo, IUserContextService userContext)
    {
        _itemRepo = itemRepo;
        _userContext = userContext;
    }

    [HttpGet("/items/create")]
    public IActionResult Create()
    {
        // Sadece admin veya student giriş yapmışsa item eklenebilir
        if (!_userContext.IsAdmin && !_userContext.IsStudent)
        {
            TempData["Message"] = "You must be logged in to create an item.";
            return RedirectToPage("/Items/Index");
        }

        // Varsayılan yeni ilan
        var newItem = new ClassifiedItem
        {
            Title = "Başlık girin.",
            Description = " Açıklama girin.",
        };

        return View(newItem); // Create.cshtml'e model gönderilir
    }

    [HttpPost("/items/create")]
    public IActionResult Create(ClassifiedItem model)
    {
        // Yine giriş kontrolü
        if (!_userContext.IsAdmin && !_userContext.IsStudent)
        {
            TempData["Message"] = "You must be logged in to create an item.";
            return RedirectToPage("/Items/Index");
        }

        // Model doğrulaması başarısızsa aynı sayfayı göster
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Otomatik olarak giriş yapan kullanıcının email adresini seller olarak ata
        model.Seller = _userContext.Email ?? "unknown";

        _itemRepo.Add(model);

        TempData["Message"] = $"Item '{model.Title}' created.";
        return RedirectToPage("/Items/Index");
    }
}
