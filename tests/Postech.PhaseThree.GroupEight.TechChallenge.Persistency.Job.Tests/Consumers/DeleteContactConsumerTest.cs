using Bogus;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Tests.Job;

public class DeleteContactConsumerTests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<ConsumeContext<DeleteContactEvent>> _consumeContextMock;
    private readonly Mock<ILogger<DeleteContactConsumer>> _loggerMock;
    private readonly Faker<DeleteContactEvent> _deleteContactEventFaker;

    public DeleteContactConsumerTests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _consumeContextMock = new Mock<ConsumeContext<DeleteContactEvent>>();
        _loggerMock = new Mock<ILogger<DeleteContactConsumer>>();

        _deleteContactEventFaker = new Faker<DeleteContactEvent>()
            .RuleFor(e => e.Id, f => f.Random.Int(1, 1000));
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Component", "Job")]
    public async Task DeleteContactConsumer_ShouldCallDeleteContactHandlerAsync_AndPublishIntegrationMessage_WhenMessageIsConsumed()
    {
        // Arrange
        var deleteContactConsumer = new DeleteContactConsumer(_contactServiceMock.Object, _publishEndpointMock.Object, _loggerMock.Object);
        var deleteEvent = _deleteContactEventFaker.Generate();
        _consumeContextMock.SetupGet(x => x.Message).Returns(deleteEvent);

        // Act
        await deleteContactConsumer.Consume(_consumeContextMock.Object);

        // Assert
        _contactServiceMock.Verify(s => s.DeleteContactHandlerAsync(deleteEvent.Id), Times.Once);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<DeleteIntegrationModel>(), default), Times.Once);
    }
}
