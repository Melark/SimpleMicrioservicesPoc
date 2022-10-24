using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data;

public interface IStockRepo
{
    Task<StockItem> CreateStockEntry(InventoryItem item, int qtyInStock);
    Task<StockItem[]> FindAll();
    Task<StockItem?> FindByIdOrDefault(Guid id);
    Task<StockItem> Update(StockItem item);
}

public class StockRepo : IStockRepo
{
    private readonly InventoryContext _inventoryContext;

    public StockRepo(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public async Task<StockItem> CreateStockEntry(InventoryItem item, int qtyInStock)
    {
        var createdResult = await _inventoryContext.Stock.AddAsync(new StockItem(item, qtyInStock));
        await _inventoryContext.SaveChangesAsync();
        return createdResult.Entity;
    }

    public async Task<StockItem[]> FindAll()
    {
        return await _inventoryContext.Stock.Include(x => x.Item).ToArrayAsync();
    }

    public async Task<StockItem?> FindByIdOrDefault(Guid id)
    {
        return await _inventoryContext.Stock
            .Include(x => x.Item)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<StockItem> Update(StockItem item)
    {
        var updateResult = _inventoryContext.Stock.Update(item);

        await _inventoryContext.SaveChangesAsync();
        return updateResult.Entity;
    }
}