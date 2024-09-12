using Worker.Persistency.Application.Services.Interfaces;
using Worker.Persistency.Core.Entities;
using Worker.Persistency.Core.Interfaces;

namespace Worker.Persistency.Application.Services;

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
