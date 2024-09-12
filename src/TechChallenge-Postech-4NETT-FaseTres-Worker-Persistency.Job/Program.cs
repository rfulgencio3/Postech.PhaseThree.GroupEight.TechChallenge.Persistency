using MassTransit;
using Microsoft.EntityFrameworkCore;
using Worker.Persistency.Application.Services;
using Worker.Persistency.Application.Services.Interfaces;
using Worker.Persistency.Core.Interfaces;
using Worker.Persistency.Infrastructure.Contexts;
using Worker.Persistency.Infrastructure.Data;
using Worker.Persistency.Job;
using Worker.Persistency.Job.Consumers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ContactManagementDbContext>(options =>
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
            });
        });

        services.AddHostedService<Start>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ContactManagementDbContext>();
    try
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Starting database migration...");

        dbContext.Database.Migrate();

        logger.LogInformation("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations.");
    }
}

await host.RunAsync();
