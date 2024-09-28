using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Producers.Interfaces;

namespace Postech.TechChallenge.Persistency.Application.Producers;

[ExcludeFromCodeCoverage]
public class IntegrationProducer(IBus bus) : IIntegrationProducer
{
    private readonly IBus _bus = bus;

    public async Task<bool> PublishAsync(ContactIntegrationModel request)
    {
        await _bus.Publish(request, ctx =>
        {
            ctx.SetRoutingKey("IntegrationEvent");
        });
        return true;
    }
}