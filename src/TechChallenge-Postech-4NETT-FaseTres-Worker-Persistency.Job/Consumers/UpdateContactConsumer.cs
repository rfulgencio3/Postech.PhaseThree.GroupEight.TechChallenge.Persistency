using MassTransit;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.IntegrationModels;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;

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
            FirstName = model.ContactFirstName,
            LastName = model.ContactLastName,
            Email = model.ContactEmail,
            PhoneNumber = model.ContactPhoneNumber,
            ModifiedAt = DateTime.UtcNow,
            Active = model.Active
        };

        await _contactService.UpdateContactHandlerAsync(contact);

        var integrationMessage = new ContactIntegrationModel
        {
            Id = model.Id,
            FirstName = model.ContactFirstName,
            LastName = model.ContactLastName,
            Email = model.ContactEmail,
            PhoneNumber = model.ContactPhoneNumber,
            ModifiedAt = DateTime.UtcNow,
            Active = model.Active,
            EventType = model.EventType,
        };

        await _publishEndpoint.Publish(integrationMessage);
        _logger.LogInformation("Published integration message for UpdateContact at: {time}", DateTimeOffset.Now);
    }
}
