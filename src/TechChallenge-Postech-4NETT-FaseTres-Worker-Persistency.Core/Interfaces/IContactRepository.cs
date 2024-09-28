using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.ValueObjects;

namespace Postech.TechChallenge.Persistency.Core.Interfaces;

public interface IContactRepository : IRepository<ContactEntity, Guid>
{
    Task<IEnumerable<ContactEntity>> GetContactsByAreaCodeValueAsync(string areaCodeValue);
    Task<AreaCodeValueObject?> GetAreaCodeByValueAsync(string areaCodeValue);
    Task<ContactPhoneValueObject?> GetContactPhoneByNumberAndAreaCodeValueAsync(string phoneNumber, string areaCodeValue);
    Task<IEnumerable<ContactEntity>> GetContactsByContactPhoneAsync(ContactPhoneValueObject contactPhone);
}