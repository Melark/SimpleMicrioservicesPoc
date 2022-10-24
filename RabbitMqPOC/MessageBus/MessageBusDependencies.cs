using Microsoft.Extensions.DependencyInjection;

namespace MessageBus
{
    public class MessageBusDependencies
    {
        public static void RegisterMessageBus(IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, RabbitClient>();
        }
    }
}