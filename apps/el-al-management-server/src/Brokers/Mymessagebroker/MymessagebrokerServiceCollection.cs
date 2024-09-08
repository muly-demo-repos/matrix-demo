using ElAlManagement.Brokers.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElAlManagement.Brokers.Mymessagebroker;

public static class MymessagebrokerServiceCollection
{
    public static IServiceCollection AddMymessagebroker(this IHostApplicationBuilder app)
    {
        var kafkaOptions = app.Configuration.GetSection("kafka").Get<KafkaOptions>();
        if (kafkaOptions == null)
            throw new Exception("KafkaOptions not found in configuration section kafka");
        if (kafkaOptions.ConsumerGroupId == null)
            throw new Exception("ConsumerGroupId not found in configuration section kafka");
        if (kafkaOptions.BootstrapServers == null)
            throw new Exception("BootstrapServers not found in configuration section kafka");
        return app
            .Services.AddHostedService(x => new MymessagebrokerConsumerService(
                x.GetRequiredService<IServiceScopeFactory>(),
                kafkaOptions
            ))
            .AddSingleton(x => new MymessagebrokerProducerService(kafkaOptions.BootstrapServers))
            .AddScoped<MymessagebrokerMessageHandlersController>();
    }
}
