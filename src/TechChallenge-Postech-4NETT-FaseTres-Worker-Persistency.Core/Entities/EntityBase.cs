namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool Active { get; set; }
}