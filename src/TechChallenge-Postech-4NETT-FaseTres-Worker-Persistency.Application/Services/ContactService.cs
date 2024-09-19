using Postech.GroupEight.TechChallenge.ContactManagement.Application.Services.Interfaces;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Application.Services;

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
        return contact.Id;
    }

    public async Task UpdateContactHandlerAsync(ContactEntity contact)
    {
        await _contactRepository.UpdateContactAsync(contact);
    }

    public async Task DeleteContactHandlerAsync(int id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }
}
