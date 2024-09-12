namespace Worker.Persistency.Application.Events;

public class UpdateContactEvent
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool Active { get; set; }
}
