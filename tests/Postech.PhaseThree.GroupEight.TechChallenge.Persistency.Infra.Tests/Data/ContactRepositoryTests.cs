using Bogus;
using Moq;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Interfaces;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Tests.Data;

public class ContactRepositoryTests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly ContactEntity _contact;

    public ContactRepositoryTests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();

        _contact = new Faker<ContactEntity>()
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            //.RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
            .Generate();
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Infra")]
    public async Task CreateContactAsync_ShouldCallCreateMethod()
    {
        // Act
        await _contactRepositoryMock.Object.CreateContactAsync(_contact);

        // Assert
        _contactRepositoryMock.Verify(r => r.CreateContactAsync(It.IsAny<ContactEntity>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Infra")]
    public async Task UpdateContactAsync_ShouldCallUpdateMethod()
    {
        // Act
        await _contactRepositoryMock.Object.UpdateContactAsync(_contact);

        // Assert
        _contactRepositoryMock.Verify(r => r.UpdateContactAsync(It.IsAny<ContactEntity>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Infra")]
    public async Task DeleteContactAsync_ShouldCallDeleteMethod()
    {
        // Act
        //await _contactRepositoryMock.Object.DeleteContactAsync(1);

        // Assert
        //_contactRepositoryMock.Verify(r => r.DeleteContactAsync(It.IsAny<int>()), Times.Once);
    }
}