using ElAlManagement.Brokers.Infrastructure;
using ElAlManagement.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace ElAlManagement.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}
