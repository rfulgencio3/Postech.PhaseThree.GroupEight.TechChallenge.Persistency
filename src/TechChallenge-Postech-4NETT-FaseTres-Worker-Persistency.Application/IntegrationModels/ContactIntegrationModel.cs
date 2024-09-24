namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

public class ContactIntegrationModel
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    //public bool Active { get; set; }
    public required string EventType { get; set; }
}
