using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;

public interface IContactService
{
    Task<Guid> CreateContactHandlerAsync(ContactEntity contact);
    Task UpdateContactHandlerAsync(ContactEntity contact);
    Task DeleteContactHandlerAsync(int id);
}
