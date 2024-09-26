using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.Interfaces;

namespace Postech.TechChallenge.Persistency.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<Guid> CreateContactHandlerAsync(ContactEntity contact)
    {
        await _contactRepository.CreateContactAsync(contact);
        return contact.ContactId;
    }

    public async Task UpdateContactHandlerAsync(ContactEntity contact)
    {
        await _contactRepository.UpdateContactAsync(contact);
    }

    public async Task DeleteContactHandlerAsync(Guid id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }

    public async Task<ContactEntity> GetContactByIdAsync(Guid id)
    {
        return  await _contactRepository.GetContactByIdAsync(id);
    }
}
