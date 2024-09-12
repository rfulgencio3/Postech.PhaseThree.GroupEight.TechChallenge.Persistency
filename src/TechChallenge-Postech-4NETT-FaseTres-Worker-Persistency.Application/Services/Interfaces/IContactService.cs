using Worker.Persistency.Core.Entities;

namespace Worker.Persistency.Application.Services.Interfaces;

public interface IContactService
{
    Task<Guid> CreateContactHandlerAsync(ContactEntity contact);
    Task UpdateContactHandlerAsync(ContactEntity contact);
    Task DeleteContactHandlerAsync(int id);
}
