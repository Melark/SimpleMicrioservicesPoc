namespace InventoryService.EventHandlers;

public class StockEventHandler: BackgroundService
{
    public StockEventHandler(IConfiguration configuration)
    {
        
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}