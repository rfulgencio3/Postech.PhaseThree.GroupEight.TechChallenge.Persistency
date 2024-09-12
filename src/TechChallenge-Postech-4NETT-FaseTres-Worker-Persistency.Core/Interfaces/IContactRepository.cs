using Worker.Persistency.Core.Entities;

namespace Worker.Persistency.Core.Interfaces
{
    public interface IContactRepository
    {
        Task CreateContactAsync(ContactEntity contact);
        Task UpdateContactAsync(ContactEntity contact);
        Task DeleteContactAsync(int id);
    }
}
