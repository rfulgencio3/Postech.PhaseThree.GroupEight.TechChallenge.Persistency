using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.Enumerators;

namespace Postech.TechChallenge.Persistency.Job.Consumers;

public class DeleteContactConsumer(IContactService contactService, IPublishEndpoint publishEndpoint, ILogger<DeleteContactConsumer> logger) : IConsumer<ContactDeletedEvent>
{
    private readonly IContactService _contactService = contactService;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly ILogger<DeleteContactConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<ContactDeletedEvent> context)
    {
        _logger.LogInformation("Received DeleteContact message at: {time}", DateTimeOffset.Now);
        ContactEntity? contact = await _contactService.GetContactByIdAsync(context.Message.ContactId);
        if (contact is not null)
        {
            await _contactService.DeleteContactHandlerAsync(contact);
            DeleteIntegrationModel integrationMessage = new()
            {
                Id = contact.Id,
                EventType = EventType.Delete
            };
            await _publishEndpoint.Publish(integrationMessage);
            _logger.LogInformation("Published integration message for DeleteContact at: {time}", DateTimeOffset.Now);
        }     
    }
}