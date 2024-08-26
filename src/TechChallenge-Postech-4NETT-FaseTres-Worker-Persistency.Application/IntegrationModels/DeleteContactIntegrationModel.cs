namespace Worker.Persistency.Application.IntegrationModels;

public class DeleteIntegrationModel
{
    public int Id { get; set; }
    public string OperationType { get; set; } // TOOD: delete enum
}
