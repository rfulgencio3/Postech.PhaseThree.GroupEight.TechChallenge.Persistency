namespace Worker.Persistency.Application.Events;

public class UpdateContactEvent
{
    public int Id { get; set; }
    public string DDD { get; set; }
    public string Number { get; set; }
    public string FullName { get; set; }
    public int Status { get; set; }
}
