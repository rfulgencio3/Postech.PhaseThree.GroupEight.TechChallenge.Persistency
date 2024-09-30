using System.Diagnostics.CodeAnalysis;
using Postech.TechChallenge.Persistency.Core.Enumerators;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

[ExcludeFromCodeCoverage]
public class DeleteIntegrationModel
{
    public Guid Id { get; set; }
    public required EventType EventType { get; set; }
}