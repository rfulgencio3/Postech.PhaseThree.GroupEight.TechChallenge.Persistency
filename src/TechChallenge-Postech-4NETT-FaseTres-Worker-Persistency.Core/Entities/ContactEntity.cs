namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class ContactEntity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public short ContactPhoneAreaCode { get; set; }
    public int ContactPhone { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public bool Active { get; set; }

    public ContactEntity() { }

    public ContactEntity(Guid contactId, string firstName, string lastName, string email, short contactPhoneAreaCode, int contactPhone)
    {
        Id = contactId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactPhoneAreaCode = contactPhoneAreaCode;
        ContactPhone = contactPhone;
        Active = true;
    }

    public void SetModifiedAt()
    {
        ModifiedAt = DateTime.UtcNow;
    }
    public void SetCreatedAt()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
