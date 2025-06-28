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
