using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;

public class CreateContactConsumer : IConsumer<CreateContactEvent>
{
    private readonly IContactService _contactService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CreateContactConsumer> _logger;

    public CreateContactConsumer(IContactService contactService, IPublishEndpoint publishEndpoint, ILogger<CreateContactConsumer> logger)
    {
        _contactService = contactService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateContactEvent> context)
    {
        _logger.LogInformation("Received CreateContact message at: {time}", DateTimeOffset.Now);

        var model = context.Message;

        var contact = new ContactEntity
        {
            FirstName = model.ContactFirstName,
            LastName = model.ContactLastName,
            Email = model.ContactEmail,
            PhoneNumber = model.ContactPhoneNumber,
            PhoneNumberAreaCode = model.ContactPhoneNumberAreaCode,
            CreatedAt = DateTime.UtcNow,
            Active = true,
        };

        var id = await _contactService.CreateContactHandlerAsync(contact);

        var integrationMessage = new ContactIntegrationModel
        {
            Id = id,
            FirstName = model.ContactFirstName,
            LastName = model.ContactLastName,
            Email = model.ContactEmail,
            PhoneNumber = model.ContactPhoneNumber,
            CreatedAt = DateTime.UtcNow,
            //Active = true,
            EventType = "create",
        };

        await _publishEndpoint.Publish(integrationMessage);
        _logger.LogInformation("Published integration message for CreateContact at: {time}", DateTimeOffset.Now);
    }
}
