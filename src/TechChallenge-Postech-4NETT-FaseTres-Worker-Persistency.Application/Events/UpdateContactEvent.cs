namespace Worker.Persistency.Application.Events;

public class UpdateContactEvent
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool Active { get; set; }
}
