using Postech.GroupEight.TechChallenge.ContactManagement.Events;

namespace Postech.TechChallenge.Persistency.Application.Producers.Interfaces;

public interface IIntegrationProducer
{
    public Task<bool> PublishAsync(ContactIntegrationModel request);
}