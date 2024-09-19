using Microsoft.EntityFrameworkCore;
using Postech.GroupEight.TechChallenge.ContactManagement.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Infrastructure.Contexts;

public class ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options) : DbContext(options)
{
    public DbSet<ContactEntity> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactManagementDbContext).Assembly);
    }
}