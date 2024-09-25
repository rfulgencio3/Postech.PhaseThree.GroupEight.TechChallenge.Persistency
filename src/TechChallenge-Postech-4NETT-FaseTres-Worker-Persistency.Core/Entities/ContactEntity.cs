namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class ContactEntity
{
    public Guid ContactId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public ContactPhoneEntity ContactPhone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public bool Active { get; set; }

    public ContactEntity(Guid contactId, string firstName, string lastName, string email, ContactPhoneEntity contactPhone)
    {
        ContactId = contactId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
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
