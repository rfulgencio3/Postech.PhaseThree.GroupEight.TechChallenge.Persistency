using Bogus;
using FluentAssertions;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Application.Services;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Interfaces;

namespace Postech.GroupEight.TechChallenge.Tests;

public class ContactServiceTest
{
    private readonly ContactService _contactService;
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Faker<ContactEntity> _contactFaker;

    public ContactServiceTest()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactService = new ContactService(_contactRepositoryMock.Object);

        _contactFaker = new Faker<ContactEntity>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber());
    }

    [Fact]
    public async Task CreateContactHandlerAsync_ShouldReturnContactId_WhenContactIsCreated()
    {
        // Arrange
        var contact = _contactFaker.Generate();
        _contactRepositoryMock.Setup(repo => repo.CreateContactAsync(It.IsAny<ContactEntity>()))
                              .Returns(Task.CompletedTask);

        // Act
        var result = await _contactService.CreateContactHandlerAsync(contact);

        // Assert
        result.Should().Be(contact.Id);
        _contactRepositoryMock.Verify(repo => repo.CreateContactAsync(It.IsAny<ContactEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateContactHandlerAsync_ShouldCallUpdateContactAsync_WhenContactIsUpdated()
    {
        // Arrange
        var contact = _contactFaker.Generate();
        _contactRepositoryMock.Setup(repo => repo.UpdateContactAsync(It.IsAny<ContactEntity>()))
                              .Returns(Task.CompletedTask);

        // Act
        await _contactService.UpdateContactHandlerAsync(contact);

        // Assert
        _contactRepositoryMock.Verify(repo => repo.UpdateContactAsync(It.IsAny<ContactEntity>()), Times.Once);
    }

    [Fact]
    public async Task DeleteContactHandlerAsync_ShouldCallDeleteContactAsync_WhenContactIsDeleted()
    {
        // Arrange
        var contactId = 1;
        _contactRepositoryMock.Setup(repo => repo.DeleteContactAsync(It.IsAny<int>()))
                              .Returns(Task.CompletedTask);

        // Act
        await _contactService.DeleteContactHandlerAsync(contactId);

        // Assert
        _contactRepositoryMock.Verify(repo => repo.DeleteContactAsync(contactId), Times.Once);
    }
}
