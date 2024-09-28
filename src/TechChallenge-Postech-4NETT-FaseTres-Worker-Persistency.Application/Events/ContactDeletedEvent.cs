using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

[ExcludeFromCodeCoverage]
public class ContactDeletedEvent
{
    public Guid ContactId { get; set; }
}