namespace Worker.Persistency.Application.Events;

public class CreateContactEvent
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public DateTime? CreatedAt { get; set; }
}
