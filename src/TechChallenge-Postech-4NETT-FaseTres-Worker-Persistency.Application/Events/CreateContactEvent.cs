namespace Worker.Persistency.Application.Events;

public class CreateContactEvent
{
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public bool Active { get; set; }
    public DateTime? CreatedAt { get; set; }
}
