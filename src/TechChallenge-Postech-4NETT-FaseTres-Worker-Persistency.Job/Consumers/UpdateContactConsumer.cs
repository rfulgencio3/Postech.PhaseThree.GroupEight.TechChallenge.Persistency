using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;

public class UpdateContactConsumer : IConsumer<ContactUpdatedEvent>
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

    public async Task Consume(ConsumeContext<ContactUpdatedEvent> context)
    {
        _logger.LogInformation("Received UpdateContact message at: {time}", DateTimeOffset.Now);

        var model = context.Message;

        var contactPhoneAreaCode = short.Parse(model.ContactPhoneNumberAreaCode.ToString());
        var contactPhone = int.Parse(model.ContactPhoneNumber.ToString());

        var contact = new ContactEntity(
            model.ContactId,
            model.ContactFirstName,
            model.ContactLastName,
            model.ContactEmail,
            contactPhoneAreaCode,
            contactPhone
        );

        contact.SetModifiedAt();

        await _contactService.UpdateContactHandlerAsync(contact);

        // Publica a mensagem de integração
        var integrationMessage = new ContactIntegrationModel
        {
            Id = model.ContactId,
            FirstName = model.ContactFirstName,
            LastName = model.ContactLastName,
            Email = model.ContactEmail,
            PhoneNumber = model.ContactPhoneNumber,
            ModifiedAt = DateTime.UtcNow,
            EventType = nameof(ContactUpdatedEvent),
        };

        await _publishEndpoint.Publish(integrationMessage);

        _logger.LogInformation("Published integration message for UpdateContact at: {time}", DateTimeOffset.Now);
    }
}
