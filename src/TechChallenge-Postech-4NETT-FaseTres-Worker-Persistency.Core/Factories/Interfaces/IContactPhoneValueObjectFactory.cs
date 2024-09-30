using Postech.TechChallenge.Persistency.Core.ValueObjects;

namespace Postech.TechChallenge.Persistency.Core.Factories.Interfaces
{
    public interface IContactPhoneValueObjectFactory
    {
        Task<ContactPhoneValueObject> CreateAsync(string phoneNumber, string areaCodeValue);
    }
}