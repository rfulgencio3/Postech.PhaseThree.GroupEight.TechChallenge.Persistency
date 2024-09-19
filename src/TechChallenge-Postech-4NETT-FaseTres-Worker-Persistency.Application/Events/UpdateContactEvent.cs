namespace Postech.GroupEight.TechChallenge.ContactManagement.Application.Events;

public class UpdateContactEvent
{
    public required Guid Id { get; set; }
    public required string ContactFirstName { get; set; }
    public required string ContactLastName { get; set; }
    public required string ContactEmail { get; set; }
    public required string ContactPhoneNumber { get; set; }
    public required bool Active { get; set; }
    public required string EventType { get; set; }
}
