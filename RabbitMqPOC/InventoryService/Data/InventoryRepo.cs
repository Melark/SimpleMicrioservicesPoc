using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data;

public interface IInventoryRepo
{
    Task<InventoryItem> CreateItem(string name);
    Task<InventoryItem[]> FindAll();
    Task<InventoryItem?> FindByIdOrDefault(Guid id);
}

public class InventoryRepo : IInventoryRepo
{
    private readonly InventoryContext _inventoryContext;

    public InventoryRepo(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public async Task<InventoryItem> CreateItem(string name)
    {
        var createdResult = await _inventoryContext.Items.AddAsync(new InventoryItem(name));
        await _inventoryContext.SaveChangesAsync();
        return createdResult.Entity;
    }

    public async Task<InventoryItem[]> FindAll()
    {
        return await _inventoryContext.Items.ToArrayAsync();
    }
    
    public async Task<InventoryItem?> FindByIdOrDefault(Guid id)
    {
        return await _inventoryContext.Items.FindAsync(id);
    }
}