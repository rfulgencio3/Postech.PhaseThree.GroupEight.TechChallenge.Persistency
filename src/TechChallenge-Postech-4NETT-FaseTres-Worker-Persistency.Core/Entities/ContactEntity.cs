using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Worker.Persistency.Core.Entities;

public class ContactEntity : EntityBase
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
}
