using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.Enumerators;
using Postech.TechChallenge.Persistency.Core.Factories.Interfaces;

namespace Postech.TechChallenge.Persistency.Job.Consumers;

public class UpdateContactConsumer(IContactService contactService, IPublishEndpoint publishEndpoint, IContactPhoneValueObjectFactory contactPhoneFactory, ILogger<UpdateContactConsumer> logger) : IConsumer<ContactUpdatedEvent>
{
    private readonly IContactService _contactService = contactService;
    private readonly IContactPhoneValueObjectFactory _contactPhoneFactory = contactPhoneFactory;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly ILogger<UpdateContactConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<ContactUpdatedEvent> context)
    {
        _logger.LogInformation("Received UpdateContact message at: {time}", DateTimeOffset.Now);
        ContactEntity? contact = await _contactService.GetContactByIdAsync(context.Message.ContactId);
        if (contact is null)
        {
            _logger.LogError("Contact with ID {ContactId} not found.", context.Message.ContactId);
            return;
        }
        contact.UpdateContactName(context.Message.ContactFirstName, context.Message.ContactLastName);
        contact.UpdateContactEmail(context.Message.ContactEmail);
        contact.UpdateContactPhone(await _contactPhoneFactory.CreateAsync(context.Message.ContactPhoneNumber, context.Message.ContactPhoneNumberAreaCode));
        await _contactService.UpdateContactHandlerAsync(contact);
        ContactIntegrationModel integrationMessage = new()
        {
            Id = contact.Id,
            FirstName = contact.ContactName.FirstName,
            LastName = contact.ContactName.LastName,
            Email = contact.ContactEmail.Value,
            PhoneNumber = contact.ContactPhone.Number,
            AreaCode = contact.ContactPhone.AreaCode.Value,
            ModifiedAt = contact.ModifiedAt,
            EventType = EventType.Update,
        };
        await _publishEndpoint.Publish(integrationMessage);
        _logger.LogInformation("Published integration message for UpdateContact at: {time}", DateTimeOffset.Now);
    }
}