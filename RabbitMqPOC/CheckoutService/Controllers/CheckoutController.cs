using CheckoutService.ServiceIntegration.InventoryService;
using Inventory;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutService.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IInventoryClient _inventoryClient;

    public CheckoutController(IInventoryClient inventoryClient)
    {
        _inventoryClient = inventoryClient;
    }

    [HttpGet]
    public async Task<InventoryItems> GetAllStockItems()
    {
        return await _inventoryClient.GetAllStockItems();
    }

    [HttpPost]
    public async Task CheckoutItems([FromBody] CheckoutItemsRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        
    }
}

public class CheckoutItemsRequest
{
    public StockItem[] Items { get; set; }
}

public class StockItem
{
    public Guid Id { get; set; }
    public int qty { get; set; }
}