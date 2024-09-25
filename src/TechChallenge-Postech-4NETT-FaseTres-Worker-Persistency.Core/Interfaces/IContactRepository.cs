using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Interfaces;

public interface IContactRepository
{
    Task CreateContactAsync(ContactEntity contact);
    Task UpdateContactAsync(ContactEntity contact);
    Task DeleteContactAsync(Guid id);
    Task<ContactEntity> GetContactByIdAsync(Guid id);
}
