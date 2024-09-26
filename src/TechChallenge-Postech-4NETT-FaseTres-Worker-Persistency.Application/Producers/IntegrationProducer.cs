using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Producers.Interfaces;

namespace Postech.TechChallenge.Persistency.Infra.Producer;

public class IntegrationProducer : IIntegrationProducer
{
    private readonly IBus _bus;

    public IntegrationProducer(IBus bus)
    {
        _bus = bus;
    }
    public async Task<bool> PublishAsync(ContactIntegrationModel request)
    {
        await _bus.Publish(request, ctx =>
        {
            ctx.SetRoutingKey("IntegrationEvent");
        });
        return true;
    }
}