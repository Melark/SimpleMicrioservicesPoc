using InventoryService.Data;

namespace InventoryService.Services;

public interface IStockService
{
    Task<StockItem[]> ListAllStock();
    Task<StockItem> ReserveStock(Guid id, int amountReserved);
    Task<StockItem> CreateStockRecord(Guid itemId, int amountReserved);
    Task<StockItem> FindSpecificEntry(Guid id);
}

public class StockService : IStockService
{
    private readonly IInventoryRepo _inventoryRepo;
    private readonly IStockRepo _stockRepo;

    public StockService(IInventoryRepo inventoryRepo, IStockRepo stockRepo)
    {
        _inventoryRepo = inventoryRepo;
        _stockRepo = stockRepo;
    }

    public async Task<StockItem[]> ListAllStock()
    {
        return await _stockRepo.FindAll();
    }
    
    public async Task<StockItem> FindSpecificEntry(Guid id)
    {
        var stockRecord = await _stockRepo.FindByIdOrDefault(id);
        if (stockRecord == null) throw new Exception("StockRecord not found");

        return stockRecord;
    }

    public async Task<StockItem> ReserveStock(Guid id, int amountReserved)
    {
        var stockItem = await _stockRepo.FindByIdOrDefault(id);
        if (stockItem == null) throw new Exception("Stock item not found");
        
        if (stockItem.QtyAvailable < amountReserved) throw new Exception("Not enough stock available");
        
        stockItem.Reserve(amountReserved);

        return await _stockRepo.Update(stockItem);
    }
    
    public async Task<StockItem> CreateStockRecord(Guid itemId, int amountReserved)
    {
        var item = await _inventoryRepo.FindByIdOrDefault(itemId);
        if (item == null) throw new Exception("Item does not exist");
        
        return await _stockRepo.CreateStockEntry(item, amountReserved);
    }
}