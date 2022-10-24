using Grpc.Net.Client;
using Inventory;

namespace CheckoutService.ServiceIntegration.InventoryService;

public interface IInventoryClient
{
    Task<InventoryItems> GetAllStockItems();
}

public class InventoryClient : IInventoryClient
{
    private readonly string _url;

    public InventoryClient(IConfiguration configuration)
    {
        _url = configuration["InventoryServiceUrl"];
    }

    public async Task<InventoryItems> GetAllStockItems()
    {
        var httpHandler = new HttpClientHandler();
        // Return `true` to allow certificates that are untrusted/invalid
        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        using var channel = GrpcChannel.ForAddress(_url, new GrpcChannelOptions {HttpHandler = httpHandler});
        var client = new Inventory.InventoryService.InventoryServiceClient(channel);

        return await client.GetAllAsync(new Empty());
    }
}