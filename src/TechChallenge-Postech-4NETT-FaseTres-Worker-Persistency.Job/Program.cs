using FluentMigrator.Runner;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Producers.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Application.Services.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Interfaces;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Contexts;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Data;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Migrations;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Producer;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.Consumers;
using Prometheus;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(context.Configuration.GetConnectionString("DefaultConnection"))
                .ScanIn(typeof(CreateContactsSchema).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        services.AddDbContext<ContactManagementDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IContactService, ContactService>();

        services.AddScoped<IIntegrationProducer, IntegrationProducer>();

        var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? context.Configuration.GetSection("RabbitMQ")["Host"];
        var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? context.Configuration.GetSection("RabbitMQ")["Username"];
        var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? context.Configuration.GetSection("RabbitMQ")["Password"];

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

        var metricsServer = new KestrelMetricServer(port: 5679);
        metricsServer.Start();

    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();

    try
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Starting FluentMigrator database migration...");

        migrationRunner.MigrateUp();

        logger.LogInformation("FluentMigrator database migration completed successfully.");
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying FluentMigrator migrations.");
    }
}

await host.RunAsync();
