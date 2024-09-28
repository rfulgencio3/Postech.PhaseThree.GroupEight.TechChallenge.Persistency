using System.Diagnostics.CodeAnalysis;
using Postech.TechChallenge.Persistency.Core.Enumerators;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

[ExcludeFromCodeCoverage]
public class ContactIntegrationModel
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AreaCode { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public required EventType EventType { get; set; }
}