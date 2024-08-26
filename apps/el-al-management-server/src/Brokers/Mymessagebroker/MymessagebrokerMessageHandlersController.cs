using System.Threading.Tasks;
using ElAlManagement.Brokers.Infrastructure;

namespace ElAlManagement.Brokers.Mymessagebroker;

public class MymessagebrokerMessageHandlersController
{
    [Topic("flight.canceled")]
    public Task Handleflight.canceled(string message)
    {
        //set your message handling logic here

        return Task.CompletedTask;
    }
}
