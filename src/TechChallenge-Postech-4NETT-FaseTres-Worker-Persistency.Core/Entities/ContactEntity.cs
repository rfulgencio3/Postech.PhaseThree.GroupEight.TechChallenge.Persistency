using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Worker.Persistency.Core.Entities;

public class ContactEntity : EntityBase
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
}
