namespace Worker.Persistency.Application.Events;

public class UpdateContactEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool Active { get; set; }
}
