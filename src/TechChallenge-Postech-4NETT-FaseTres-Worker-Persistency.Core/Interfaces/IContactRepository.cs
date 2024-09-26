using Postech.TechChallenge.Persistency.Core.Entities;

namespace Postech.TechChallenge.Persistency.Core.Interfaces;

public interface IContactRepository
{
    Task CreateContactAsync(ContactEntity contact);
    Task UpdateContactAsync(ContactEntity contact);
    Task DeleteContactAsync(Guid id);
    Task<ContactEntity> GetContactByIdAsync(Guid id);
}
