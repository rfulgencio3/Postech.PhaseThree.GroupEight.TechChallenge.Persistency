using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Worker.Persistency.Core.Entities;

public class ContactEntity : EntityBase
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
}
