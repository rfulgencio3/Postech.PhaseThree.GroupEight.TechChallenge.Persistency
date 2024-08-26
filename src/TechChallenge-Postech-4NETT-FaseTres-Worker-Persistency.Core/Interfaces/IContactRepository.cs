using Worker.Persistency.Core.Entities;

namespace Worker.Persistency.Core.Interfaces
{
    public interface IContactRepository
    {
        Task CreateContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}
