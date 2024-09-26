using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities.Enums;

namespace Postech.TechChallenge.Persistency.Job.Consumers;

public class DeleteContactConsumer : IConsumer<ContactDeletedEvent>
{
    private readonly IContactService _contactService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<DeleteContactConsumer> _logger;

    public DeleteContactConsumer(IContactService contactService, IPublishEndpoint publishEndpoint, ILogger<DeleteContactConsumer> logger)
    {
        _contactService = contactService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ContactDeletedEvent> context)
    {
        _logger.LogInformation("Received DeleteContact message at: {time}", DateTimeOffset.Now);

        var model = context.Message;

        await _contactService.DeleteContactHandlerAsync(model.ContactId);

        var integrationMessage = new DeleteIntegrationModel
        {
            Id = model.ContactId,
            EventType = EventType.Delete
        };

        await _publishEndpoint.Publish(integrationMessage);
        _logger.LogInformation("Published integration message for DeleteContact at: {time}", DateTimeOffset.Now);
    }
}
