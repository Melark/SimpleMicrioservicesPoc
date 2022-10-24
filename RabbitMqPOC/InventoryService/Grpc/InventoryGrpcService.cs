using Grpc.Core;
using Inventory;
using InventoryService.Services;

namespace InventoryService.Grpc;

public class InventoryGrpcService : Inventory.InventoryService.InventoryServiceBase
{
    private readonly IStockService _stockService;

    public InventoryGrpcService(IStockService stockService)
    {
        _stockService = stockService;
        ;
    }

    public override async Task<InventoryItems> GetAll(Empty request, ServerCallContext context)
    {
        var availableStock = await _stockService.ListAllStock();
        return new InventoryItems
        {
            Items =
            {
                availableStock.Select(x => new InventoryItem
                {
                    ItemId = x.Id.ToString(),
                    AvailableAmount = x.QtyAvailable,
                    ItemName = x.Item.Name
                }).ToArray()
            }
        };
    }
}