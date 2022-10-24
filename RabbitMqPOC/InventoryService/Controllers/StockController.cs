using InventoryService.Data;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;
    private readonly IStockService _stockService;

    public StockController(ILogger<InventoryController> logger, IStockService stockService)
    {
        _logger = logger;
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<StockItem[]> Get()
    {
        return await _stockService.ListAllStock();
    }
    
    [HttpGet("{id}")]
    public async Task<StockItem> GetById(Guid id)
    {
        return await _stockService.FindSpecificEntry(id);
    }
    
    [HttpPost]
    public async Task<StockItem> CreateItem(CreateStockEntryRequest request)
    {
        return await _stockService.CreateStockRecord(request.ItemId,request.QtyAvailable);
    }
    
    [HttpPut]
    public async Task<StockItem> ReserveItems(ReserveStockRequest request)
    {
        return await _stockService.ReserveStock(request.StockEntryId,request.AmountToReserve);
    }
}

public class CreateStockEntryRequest
{
    public Guid ItemId { get; set; }
    public int QtyAvailable { get; set; }
}

public class ReserveStockRequest
{
    public Guid StockEntryId { get; set; }
    public int AmountToReserve { get; set; }
}