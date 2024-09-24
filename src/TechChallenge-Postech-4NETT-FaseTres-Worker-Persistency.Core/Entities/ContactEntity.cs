using System.ComponentModel.DataAnnotations.Schema;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class ContactEntity : EntityBase
{
    [Column("contact_first_name")]
    public required string FirstName { get; init; }
    [Column("contact_last_name")]
    public required string LastName { get; init; }
    [Column("contact_email")]
    public required string Email { get; init; }
    [Column("contact_contact")]
    public required string PhoneNumber { get; init; }
    [Column("contact_contact_area_code")]
    public required string PhoneNumberAreaCode { get; init; }
}
