using Postech.GroupEight.TechChallenge.ContactManagement.Events;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Producers.Interfaces;

public interface IIntegrationProducer
{
    public Task<bool> PublishAsync(ContactIntegrationModel request);
}
