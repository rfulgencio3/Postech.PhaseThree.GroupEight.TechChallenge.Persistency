using Bogus;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Job.Consumers;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Tests.Job;

public class CreateContactConsumerTests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<ConsumeContext<CreateContactEvent>> _consumeContextMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<ILogger<CreateContactConsumer>> _loggerMock;
    private readonly CreateContactConsumer _createContactConsumer;
    private readonly Faker<CreateContactEvent> _contactEventFaker;

    public CreateContactConsumerTests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _consumeContextMock = new Mock<ConsumeContext<CreateContactEvent>>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _loggerMock = new Mock<ILogger<CreateContactConsumer>>();

        _contactEventFaker = new Faker<CreateContactEvent>()
            .RuleFor(c => c.ContactFirstName, f => f.Name.FirstName())
            .RuleFor(c => c.ContactLastName, f => f.Name.LastName())
            .RuleFor(c => c.ContactEmail, f => f.Internet.Email())
            .RuleFor(c => c.ContactPhoneNumber, f => f.Phone.PhoneNumber());
            //.RuleFor(c => c.EventType, "create");

        //_createContactConsumer = new CreateContactConsumer(_contactServiceMock.Object, _publishEndpointMock.Object, _loggerMock.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Job")]
    public async Task CreateContactConsumer_ShouldCallCreateContactHandlerAsync_WhenMessageIsConsumed()
    {
        // Arrange
        var contactEvent = _contactEventFaker.Generate();
        _consumeContextMock.SetupGet(x => x.Message).Returns(contactEvent);

        // Act
        await _createContactConsumer.Consume(_consumeContextMock.Object);

        // Assert
        _contactServiceMock.Verify(s => s.CreateContactHandlerAsync(It.IsAny<ContactEntity>()), Times.Once);
    }
}
