namespace Worker.Persistency.Application.IntegrationModels;

public class ContactIntegrationModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool Active { get; set; }
    public required string OperationType { get; set; }
}
