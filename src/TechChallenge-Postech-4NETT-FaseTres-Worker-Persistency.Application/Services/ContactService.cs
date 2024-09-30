using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.Interfaces;

namespace Postech.TechChallenge.Persistency.Application.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Guid> CreateContactHandlerAsync(ContactEntity contact)
    {
        await _contactRepository.InsertAsync(contact);
        await _contactRepository.SaveChangesAsync();
        return contact.Id;
    }

    public async Task UpdateContactHandlerAsync(ContactEntity contact)
    {
        _contactRepository.Update(contact);
        await _contactRepository.SaveChangesAsync();
    }

    public async Task DeleteContactHandlerAsync(ContactEntity contact)
    {
        contact.Inactivate();
        await _contactRepository.SaveChangesAsync();
    }

    public async Task<ContactEntity?> GetContactByIdAsync(Guid id)
    {
        return await _contactRepository.GetByIdAsync(id);
    }
}