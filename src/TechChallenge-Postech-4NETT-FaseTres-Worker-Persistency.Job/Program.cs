using MassTransit;
using Microsoft.EntityFrameworkCore;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.TechChallenge.Persistency.Application.Producers;
using Postech.TechChallenge.Persistency.Application.Producers.Interfaces;
using Postech.TechChallenge.Persistency.Application.Services;
using Postech.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.TechChallenge.Persistency.Core.Factories;
using Postech.TechChallenge.Persistency.Core.Factories.Interfaces;
using Postech.TechChallenge.Persistency.Core.Interfaces;
using Postech.TechChallenge.Persistency.Infra.Data.Contexts;
using Postech.TechChallenge.Persistency.Infra.Data.Repositories;
using Postech.TechChallenge.Persistency.Job;
using Postech.TechChallenge.Persistency.Job.Consumers;
using Prometheus;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        IHostEnvironment env = context.HostingEnvironment;
        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ContactManagementDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IContactPhoneValueObjectFactory, ContactPhoneValueObjectFactory>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IIntegrationProducer, IntegrationProducer>();
        string? rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? context.Configuration.GetSection("RabbitMQ")["Host"];
        string? rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? context.Configuration.GetSection("RabbitMQ")["Username"];
        string? rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? context.Configuration.GetSection("RabbitMQ")["Password"];
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateContactConsumer>();
            x.AddConsumer<UpdateContactConsumer>();
            x.AddConsumer<DeleteContactConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqHost, "/", host =>
                {
                    host.Username(rabbitMqUser!);
                    host.Password(rabbitMqPassword!);
                });
                cfg.ReceiveEndpoint("contact.create", e =>
                {
                    e.Bind("contact.management", e =>
                    {
                        e.RoutingKey = "CreateContactEvent";
                        e.ExchangeType = "direct";
                    });
                    e.UseRawJsonDeserializer();
                    e.ConfigureConsumer<CreateContactConsumer>(context);
                    e.SetQueueArgument("x-dead-letter-exchange", "contact.create.dlq");
                });
                cfg.ReceiveEndpoint("contact.update", e =>
                {
                    e.Bind("contact.management", e =>
                    {
                        e.RoutingKey = "ContactUpdatedEvent";
                        e.ExchangeType = "direct";
                    });
                    e.UseRawJsonDeserializer();
                    e.ConfigureConsumer<UpdateContactConsumer>(context);
                    e.SetQueueArgument("x-dead-letter-exchange", "contact.update.dlq");
                });
                cfg.ReceiveEndpoint("contact.delete", e =>
                {
                    e.Bind("contact.management", e =>
                    {
                        e.RoutingKey = "ContactDeletedEvent";
                        e.ExchangeType = "direct";
                    });
                    e.UseRawJsonDeserializer();
                    e.ConfigureConsumer<DeleteContactConsumer>(context);
                    e.SetQueueArgument("x-dead-letter-exchange", "contact.delete.dlq");
                });
                cfg.ReceiveEndpoint("contact.create.dlq", e => { e.UseRawJsonDeserializer(); });
                cfg.ReceiveEndpoint("contact.update.dlq", e => { e.UseRawJsonDeserializer(); });
                cfg.ReceiveEndpoint("contact.delete.dlq", e => { e.UseRawJsonDeserializer(); });
                cfg.Message<ContactIntegrationModel>(e =>
                {
                    e.SetEntityName("contact.integration");
                });
            });
        });
        services.AddHostedService<Start>();
        KestrelMetricServer metricsServer = new(port: 5679);
        metricsServer.Start();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

await host.RunAsync();