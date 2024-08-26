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

    public async Task<int> CreateContactHandlerAsync(Contact contact)
    {
        await _contactRepository.CreateContactAsync(contact);
        return contact.Id; // Retorna o ID do contato criado
    }

    public async Task UpdateContactHandlerAsync(Contact contact)
    {
        await _contactRepository.UpdateContactAsync(contact);
    }

    public async Task DeleteContactHandlerAsync(int id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }
}
