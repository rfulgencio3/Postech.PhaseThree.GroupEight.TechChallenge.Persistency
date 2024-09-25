using Bogus;
using FluentAssertions;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.GroupEight.TechChallenge.Tests.Core;

public class ContactEntityTest
{
    private readonly Faker<ContactEntity> _faker;

    public ContactEntityTest()
    {
        _faker = new Faker<ContactEntity>()
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, f => f.Internet.Email());
            //.RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber());
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Core")]
    public void ContactEntity_ShouldBeCreatedWithValidData()
    {
        // Arrange
        var contact = _faker.Generate();

        // Assert
        contact.FirstName.Should().NotBeNullOrEmpty();
        contact.LastName.Should().NotBeNullOrEmpty();
        contact.Email.Should().NotBeNullOrEmpty();
        //contact.PhoneNumber.Should().NotBeNullOrEmpty();
    }
}
