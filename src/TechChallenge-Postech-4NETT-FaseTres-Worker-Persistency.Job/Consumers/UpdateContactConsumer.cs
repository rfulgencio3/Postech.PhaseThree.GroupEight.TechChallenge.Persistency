using MassTransit;
using Worker.Persistency.Application.Events;
using Worker.Persistency.Application.IntegrationModels;
using Worker.Persistency.Application.Services.Interfaces;
using Worker.Persistency.Core.Entities;

namespace Worker.Persistency.Job.Consumers;

public class UpdateContactConsumer : IConsumer<UpdateContactEvent>
{
    private readonly IContactService _contactService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UpdateContactConsumer> _logger;

    public UpdateContactConsumer(IContactService contactService, IPublishEndpoint publishEndpoint, ILogger<UpdateContactConsumer> logger)
    {
        _contactService = contactService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UpdateContactEvent> context)
    {
        _logger.LogInformation("Received UpdateContact message at: {time}", DateTimeOffset.Now);

        var model = context.Message;

        var contact = new ContactEntity
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = model.Phone,
            Email = model.Email,
            ModifiedAt = model.ModifiedAt,
            Active = model.Active
        };

        await _contactService.UpdateContactHandlerAsync(contact);

        var integrationMessage = new ContactIntegrationModel
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = model.Phone,
            Email = model.Email,
            ModifiedAt = model.ModifiedAt,
            Active = model.Active,
            EventType = "update"
        };

        await _publishEndpoint.Publish(integrationMessage);
        _logger.LogInformation("Published integration message for UpdateContact at: {time}", DateTimeOffset.Now);
    }
}
