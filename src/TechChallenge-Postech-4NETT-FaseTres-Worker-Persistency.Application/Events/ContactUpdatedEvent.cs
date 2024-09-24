namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

public class ContactUpdatedEvent
{
    public required Guid ContactId { get; set; }
    public required string ContactFirstName { get; set; }
    public required string ContactLastName { get; set; }
    public required string ContactEmail { get; set; }
    public required string ContactPhoneNumber { get; set; }
    public required string ContactPhoneNumberAreaCode { get; set; }
}
