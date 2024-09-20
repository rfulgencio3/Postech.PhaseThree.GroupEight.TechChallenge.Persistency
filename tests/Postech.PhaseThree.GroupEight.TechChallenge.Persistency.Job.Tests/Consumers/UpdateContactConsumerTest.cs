using Bogus;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.IntegrationModels;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Tests.Job;

public class UpdateContactConsumerTests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<ConsumeContext<UpdateContactEvent>> _consumeContextMock;
    private readonly Mock<ILogger<UpdateContactConsumer>> _loggerMock;
    private readonly Faker<UpdateContactEvent> _updateContactEventFaker;

    public UpdateContactConsumerTests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _consumeContextMock = new Mock<ConsumeContext<UpdateContactEvent>>();
        _loggerMock = new Mock<ILogger<UpdateContactConsumer>>();

        _updateContactEventFaker = new Faker<UpdateContactEvent>()
            .RuleFor(e => e.Id, Guid.NewGuid())
            .RuleFor(e => e.ContactFirstName, f => f.Name.FirstName())
            .RuleFor(e => e.ContactLastName, f => f.Name.LastName())
            .RuleFor(e => e.ContactEmail, f => f.Internet.Email())
            .RuleFor(e => e.ContactPhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.Active, f => f.Random.Bool())
            .RuleFor(e => e.EventType, "update");
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Job")]
    public async Task UpdateContactConsumer_ShouldCallUpdateContactHandlerAsync_AndPublishIntegrationMessage_WhenMessageIsConsumed()
    {
        // Arrange
        var updateContactConsumer = new UpdateContactConsumer(_contactServiceMock.Object, _publishEndpointMock.Object, _loggerMock.Object);
        var updateEvent = _updateContactEventFaker.Generate();
        _consumeContextMock.SetupGet(x => x.Message).Returns(updateEvent);

        // Act
        await updateContactConsumer.Consume(_consumeContextMock.Object);

        // Assert
        _contactServiceMock.Verify(s => s.UpdateContactHandlerAsync(It.IsAny<ContactEntity>()), Times.Once);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<ContactIntegrationModel>(), default), Times.Once);
    }
}
