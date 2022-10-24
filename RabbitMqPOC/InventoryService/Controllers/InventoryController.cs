using InventoryService.Data;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;
    private readonly IInventoryService _inventoryService;

    public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<InventoryItem[]> Get()
    {
        return await _inventoryService.ListSelectableInventoryItems();
    }
    
    [HttpGet("{id}")]
    public async Task<InventoryItem> GetById(Guid id)
    {
        return await _inventoryService.FindSpecificItem(id);
    }
    
    [HttpPost]
    public async Task<InventoryItem> CreateItem(CreateInventoryItemRequest request)
    {
        return await _inventoryService.CreateSelectableInventory(request.Name);
    }
}

public class CreateInventoryItemRequest
{
    public string Name { get; set; }
}