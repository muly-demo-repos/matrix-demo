using ElAlManagement.Brokers.Infrastructure;

namespace ElAlManagement.Brokers.Mymessagebroker;

public class MymessagebrokerProducerService : InternalProducer
{
    public MymessagebrokerProducerService(string bootstrapServers)
        : base(bootstrapServers) { }
}
