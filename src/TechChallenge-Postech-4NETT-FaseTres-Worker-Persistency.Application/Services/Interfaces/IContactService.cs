using Postech.GroupEight.TechChallenge.ContactManagement.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Application.Services.Interfaces;

public interface IContactService
{
    Task<Guid> CreateContactHandlerAsync(ContactEntity contact);
    Task UpdateContactHandlerAsync(ContactEntity contact);
    Task DeleteContactHandlerAsync(int id);
}
