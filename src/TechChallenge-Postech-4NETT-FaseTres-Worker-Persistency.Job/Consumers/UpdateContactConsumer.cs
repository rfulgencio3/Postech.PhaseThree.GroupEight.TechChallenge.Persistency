using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities.Enums;

namespace Postech.TechChallenge.Persistency.Job.Consumers;

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

        var existingContact = await _contactService.GetContactByIdAsync(model.ContactId);

        if (existingContact == null)
        {
            _logger.LogError("Contact with ID {ContactId} not found.", model.ContactId);
            return;
        }

        existingContact.UpdateFirstName(!string.IsNullOrWhiteSpace(model.ContactFirstName) ? model.ContactFirstName : existingContact.FirstName);
        existingContact.UpdateLastName(!string.IsNullOrWhiteSpace(model.ContactLastName) ? model.ContactLastName : existingContact.LastName);
        existingContact.UpdateEmail(!string.IsNullOrWhiteSpace(model.ContactEmail) ? model.ContactEmail : existingContact.Email);

        existingContact.UpdatePhone(
            short.TryParse(model.ContactPhoneNumberAreaCode, out var areaCode) ? areaCode : existingContact.ContactPhoneAreaCode,
            int.TryParse(model.ContactPhoneNumber, out var phone) ? phone : existingContact.ContactPhone
        );

        existingContact.SetModifiedAt();

        await _contactService.UpdateContactHandlerAsync(existingContact);

        var integrationMessage = new ContactIntegrationModel
        {
            Id = model.ContactId,
            FirstName = existingContact.FirstName,
            LastName = existingContact.LastName,
            Email = existingContact.Email,
            PhoneNumber = model.ContactPhoneNumber,
            ModifiedAt = existingContact.ModifiedAt,
            EventType = EventType.Update,
        };

        await _publishEndpoint.Publish(integrationMessage);

        _logger.LogInformation("Published integration message for UpdateContact at: {time}", DateTimeOffset.Now);
    }
}

