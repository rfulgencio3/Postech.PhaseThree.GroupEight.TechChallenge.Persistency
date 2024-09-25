namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class ContactEntity
{
    public Guid ContactId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public short ContactPhoneAreaCode { get; private set; }
    public int ContactPhone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public bool Active { get; set; }

    public ContactEntity(Guid contactId, string firstName, string lastName, string email, short contactPhoneAreaCode, int contactPhone)
    {
        ContactId = contactId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactPhoneAreaCode = contactPhoneAreaCode;
        ContactPhone = contactPhone;
        Active = true;
    }

    public void UpdateFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void UpdateLastName(string lastName)
    {
        LastName = lastName;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdatePhone(short areaCode, int phone)
    {
        ContactPhoneAreaCode = areaCode;
        ContactPhone = phone;
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
