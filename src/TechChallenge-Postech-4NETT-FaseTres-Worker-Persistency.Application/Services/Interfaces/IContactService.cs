using Postech.TechChallenge.Persistency.Core.Entities;

namespace Postech.TechChallenge.Persistency.Application.Services.Interfaces;

public interface IContactService
{
    Task<Guid> CreateContactHandlerAsync(ContactEntity contact);
    Task UpdateContactHandlerAsync(ContactEntity contact);
    Task DeleteContactHandlerAsync(ContactEntity contact);
    Task<ContactEntity?> GetContactByIdAsync(Guid id);
}