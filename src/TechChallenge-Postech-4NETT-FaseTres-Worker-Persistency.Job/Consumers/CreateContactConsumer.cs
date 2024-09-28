using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Producers.Interfaces;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.Factories.Interfaces;
using Postech.TechChallenge.Persistency.Core.Enumerators;
using Postech.TechChallenge.Persistency.Core.ValueObjects;

namespace Postech.TechChallenge.Persistency.Job.Consumers;

public class CreateContactConsumer(IContactService contactService, IIntegrationProducer producer, IContactPhoneValueObjectFactory contactPhoneFactory, ILogger<CreateContactConsumer> logger) : IConsumer<CreateContactEvent>
{
    private readonly IContactService _contactService = contactService;
    private readonly IContactPhoneValueObjectFactory _contactPhoneFactory = contactPhoneFactory;
    private readonly IIntegrationProducer _producer = producer;
    private readonly ILogger<CreateContactConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<CreateContactEvent> context)
    {
        _logger.LogInformation("Received CreateContact message at: {time}", DateTimeOffset.Now);
        ContactNameValueObject contactName = new(context.Message.ContactFirstName, context.Message.ContactLastName);
        ContactEmailValueObject contactEmail = new(context.Message.ContactEmail);
        ContactPhoneValueObject contactPhone = await _contactPhoneFactory.CreateAsync(context.Message.ContactPhoneNumber, context.Message.ContactPhoneNumberAreaCode);
        ContactEntity contact = new(contactName, contactEmail, contactPhone);
        _ = await _contactService.CreateContactHandlerAsync(contact);
        ContactIntegrationModel integrationMessage = new()
        {
            Id = contact.Id,
            FirstName = contact.ContactName.FirstName,
            LastName = contact.ContactName.LastName,
            Email = contact.ContactEmail.Value,
            PhoneNumber = contact.ContactPhone.Number,
            AreaCode = contact.ContactPhone.AreaCode.Value,
            ModifiedAt = contact.ModifiedAt,
            CreatedAt = contact.CreatedAt,
            EventType = EventType.Create,
        };
        await _producer.PublishAsync(integrationMessage);
        _logger.LogInformation("Published integration message for CreateContact at: {time}", DateTimeOffset.Now);
    }
}