using InventoryService.Data;

namespace InventoryService.Services;

public interface IInventoryService
{
    Task<InventoryItem[]> ListSelectableInventoryItems();
    Task<InventoryItem> CreateSelectableInventory(string name);
    Task<InventoryItem> FindSpecificItem(Guid id);
}

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepo _inventoryRepo;

    public InventoryService(IInventoryRepo inventoryRepo)
    {
        _inventoryRepo = inventoryRepo;
    }

    public async Task<InventoryItem[]> ListSelectableInventoryItems()
    {
        return await _inventoryRepo.FindAll();
    }
    
    public async Task<InventoryItem> FindSpecificItem(Guid id)
    {
        var item = await _inventoryRepo.FindByIdOrDefault(id);

        if (item == null) throw new Exception("Item does not exist");

        return item;
    }
    
    public async Task<InventoryItem> CreateSelectableInventory(string name)
    {
        return await _inventoryRepo.CreateItem(name);
    }
}