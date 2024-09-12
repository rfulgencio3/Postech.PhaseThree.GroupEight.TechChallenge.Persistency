using Microsoft.EntityFrameworkCore;
using Worker.Persistency.Core.Entities;

namespace Worker.Persistency.Infrastructure.Context;

public class ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options) : DbContext(options)
{
    public DbSet<ContactEntity> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactManagementDbContext).Assembly);
    }
}