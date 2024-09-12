namespace Worker.Persistency.Application.IntegrationModels;

public class DeleteIntegrationModel
{
    public int Id { get; set; }
    public required string OperationType { get; set; }
}
