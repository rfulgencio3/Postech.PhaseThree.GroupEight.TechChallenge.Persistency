using Postech.TechChallenge.Persistency.Core.Entities.Enums;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

public class DeleteIntegrationModel
{
    public Guid Id { get; set; }
    public required EventType EventType { get; set; }
}
