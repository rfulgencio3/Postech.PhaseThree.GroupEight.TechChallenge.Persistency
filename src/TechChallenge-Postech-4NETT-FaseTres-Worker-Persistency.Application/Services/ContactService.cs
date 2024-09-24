using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Interfaces;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services;

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

    public async Task DeleteContactHandlerAsync(int id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }
}
