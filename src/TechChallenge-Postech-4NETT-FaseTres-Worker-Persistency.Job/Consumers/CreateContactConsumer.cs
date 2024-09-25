using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Producers.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;

public class CreateContactConsumer : IConsumer<CreateContactEvent>
{
    private readonly IContactService _contactService;
    private readonly IIntegrationProducer _producer;
    private readonly ILogger<CreateContactConsumer> _logger;

    public CreateContactConsumer(IContactService contactService, IIntegrationProducer producer, ILogger<CreateContactConsumer> logger)
    {
        _contactService = contactService;
        _producer = producer;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateContactEvent> context)
    {
        _logger.LogInformation("Received CreateContact message at: {time}", DateTimeOffset.Now);

        var model = context.Message;
        var contactId = Guid.NewGuid();

        var areaCode = new AreaCodeEntity(model.ContactPhoneNumberAreaCode);
        var contactPhone = new ContactPhoneEntity(model.ContactPhoneNumber, areaCode);

        var contact = new ContactEntity(
            contactId,
            model.ContactFirstName,
            model.ContactLastName,
            model.ContactEmail,
            contactPhone
        );

        contact.SetCreatedAt();

        var id = await _contactService.CreateContactHandlerAsync(contact);

        var integrationMessage = new ContactIntegrationModel
        {
            Id = id,
            FirstName = model.ContactFirstName,
            LastName = model.ContactLastName,
            Email = model.ContactEmail,
            PhoneNumber = model.ContactPhoneNumber,
            CreatedAt = DateTime.UtcNow,
            EventType = nameof(CreateContactEvent),
        };

        await _producer.PublishAsync(integrationMessage);

        _logger.LogInformation("Published integration message for CreateContact at: {time}", DateTimeOffset.Now);
    }
}
