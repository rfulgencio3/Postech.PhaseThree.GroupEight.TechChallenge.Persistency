using Postech.GroupEight.TechChallenge.ContactManagement.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Core.Interfaces
{
    public interface IContactRepository
    {
        Task CreateContactAsync(ContactEntity contact);
        Task UpdateContactAsync(ContactEntity contact);
        Task DeleteContactAsync(int id);
    }
}
