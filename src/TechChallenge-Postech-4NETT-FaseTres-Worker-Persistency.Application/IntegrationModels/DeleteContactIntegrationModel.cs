namespace Postech.GroupEight.TechChallenge.ContactManagement.Events;

public class DeleteIntegrationModel
{
    public Guid Id { get; set; }
    public required string OperationType { get; set; }
}
