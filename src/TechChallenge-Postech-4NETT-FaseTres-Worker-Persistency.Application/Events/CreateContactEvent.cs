namespace Worker.Persistency.Application.Events;

public class CreateContactEvent
{
    public string DDD { get; set; }
    public string Number { get; set; }
    public string FullName { get; set; }
    public int Status { get; set; }
}
