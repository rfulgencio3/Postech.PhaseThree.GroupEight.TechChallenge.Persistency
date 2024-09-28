using Postech.TechChallenge.Persistency.Core.Exceptions.Common;
using Postech.TechChallenge.Persistency.Core.Factories.Interfaces;
using Postech.TechChallenge.Persistency.Core.Interfaces;
using Postech.TechChallenge.Persistency.Core.ValueObjects;

namespace Postech.TechChallenge.Persistency.Core.Factories
{
    public class ContactPhoneValueObjectFactory(IContactRepository contactRepository) : IContactPhoneValueObjectFactory
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<ContactPhoneValueObject> CreateAsync(string phoneNumber, string areaCodeValue)
        {
            ContactPhoneValueObject? contactPhone = await _contactRepository.GetContactPhoneByNumberAndAreaCodeValueAsync(phoneNumber, areaCodeValue);
            if (contactPhone is not null)
            {
                return contactPhone;
            }
            AreaCodeValueObject? areaCode = await _contactRepository.GetAreaCodeByValueAsync(areaCodeValue);
            NotFoundException.ThrowWhenNullEntity(areaCode, "The area code was not found.");
            return new(phoneNumber, areaCode);
        }
    }
}