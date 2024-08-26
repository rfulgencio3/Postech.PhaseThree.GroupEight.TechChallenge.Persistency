using Worker.Persistency.Core.Entities;

namespace Worker.Persistency.Application.Services.Interfaces;

public interface IContactService
{
    Task<int> CreateContactHandlerAsync(Contact contact);
    Task UpdateContactHandlerAsync(Contact contact);
    Task DeleteContactHandlerAsync(int id);
}
