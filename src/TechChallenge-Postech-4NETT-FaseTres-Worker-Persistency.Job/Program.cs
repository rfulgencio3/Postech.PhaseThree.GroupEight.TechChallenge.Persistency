using MassTransit;
using Microsoft.EntityFrameworkCore;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infrastructure;
using System.Text.Json;
using Worker.Persistency.Application.Services;
using Worker.Persistency.Application.Services.Interfaces;
using Worker.Persistency.Core.Interfaces;
using Worker.Persistency.Infrastructure.Data;
using Worker.Persistency.Job;
using Worker.Persistency.Job.Consumers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ContactDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IContactService, ContactService>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateContactConsumer>();
            x.AddConsumer<UpdateContactConsumer>();
            x.AddConsumer<DeleteContactConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", host => { });

                cfg.ConfigureJsonSerializerOptions(options =>
                {
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    return options;
                });

                cfg.ReceiveEndpoint("contact.create", e =>
                {
                    e.ConfigureConsumer<CreateContactConsumer>(context);
                });

                cfg.ReceiveEndpoint("contact.update", e =>
                {
                    e.ConfigureConsumer<UpdateContactConsumer>(context);
                });

                cfg.ReceiveEndpoint("contact.delete", e =>
                {
                    e.ConfigureConsumer<DeleteContactConsumer>(context);
                });

                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });

        services.AddMassTransitHostedService(true);

        services.AddHostedService<Start>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

await host.RunAsync();
