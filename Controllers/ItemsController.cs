using Microsoft.AspNetCore.Mvc;

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
        return Ok(items); // JSON döner
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
        if (!_userContext.IsAdmin && !_userContext.IsStudent)
        {
            TempData["Message"] = "You must be logged in to create an item.";
            return RedirectToPage("/Items/Index");
        }

        var newItem = new ClassifiedItem
        {
            Title = "Başlık girin.",
            Description = " Açıklama girin.",
        };

        return View(newItem);
    }

    [HttpPost("/items/create")]
    public IActionResult Create(ClassifiedItem model)
    {
        if (!_userContext.IsAdmin && !_userContext.IsStudent)
        {
            TempData["Message"] = "You must be logged in to create an item.";
            return RedirectToPage("/Items/Index");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Seller'ı otomatik login email olarak ata
        model.Seller = _userContext.Email ?? "unknown";

        _itemRepo.Add(model);

        TempData["Message"] = $"Item '{model.Title}' created.";
        return RedirectToPage("/Items/Index");
    }
}