using Bogus;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Postech.GroupEight.TechChallenge.Tests.Infrastructure;

public class ContactRepositoryTest
{
    private readonly Mock<IContactRepository> _repositoryMock;
    private readonly ContactEntity _contact;

    public ContactRepositoryTest()
    {
        _repositoryMock = new Mock<IContactRepository>();

        // Mocking a ContactEntity using Bogus
        _contact = new Faker<ContactEntity>()
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
            .Generate();
    }

    [Fact]
    public async Task CreateContactAsync_ShouldCallCreateMethod()
    {
        // Act
        await _repositoryMock.Object.CreateContactAsync(_contact);

        // Assert
        _repositoryMock.Verify(r => r.CreateContactAsync(It.IsAny<ContactEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateContactAsync_ShouldCallUpdateMethod()
    {
        // Act
        await _repositoryMock.Object.UpdateContactAsync(_contact);

        // Assert
        _repositoryMock.Verify(r => r.UpdateContactAsync(It.IsAny<ContactEntity>()), Times.Once);
    }

    [Fact]
    public async Task DeleteContactAsync_ShouldCallDeleteMethod()
    {
        // Act
        await _repositoryMock.Object.DeleteContactAsync(1);

        // Assert
        _repositoryMock.Verify(r => r.DeleteContactAsync(It.IsAny<int>()), Times.Once);
    }
}
