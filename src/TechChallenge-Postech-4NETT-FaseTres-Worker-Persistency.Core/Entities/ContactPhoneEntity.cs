using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class ContactPhoneEntity(string phoneNumber, AreaCodeEntity areaCode)
{
    public int ContactPhoneId { get; private set; }
    public string PhoneNumber { get; private set; } = phoneNumber;
    public AreaCodeEntity AreaCode { get; private set; } = areaCode;
}
