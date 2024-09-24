using System.ComponentModel.DataAnnotations.Schema;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class EntityBase
{
    [Column("contact_id")]
    public Guid Id { get; set; }
    [Column("contact_created")]
    public DateTime CreatedAt { get; set; }
    [Column("contact_modified")]
    public DateTime? ModifiedAt { get; set; }
    [Column("contact_active")]
    public bool Active { get; set; }
}