namespace Worker.Persistency.Application.IntegrationModels;

public class ContactIntegrationModel
{
    public int Id { get; set; }
    public string DDD { get; set; }
    public string Number { get; set; }
    public string FullName { get; set; }
    public int Status { get; set; }
    public string OperationType { get; set; } // TODO: create, update enum
}
