using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.ValueObjects;

namespace Postech.TechChallenge.Persistency.Infra.Data.Contexts;

[ExcludeFromCodeCoverage]
public class ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options) : DbContext(options)
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<ContactPhoneValueObject> ContactPhones { get; set; }
    public DbSet<RegionValueObject> Regions { get; set; }
    public DbSet<AreaCodeValueObject> AreaCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactManagementDbContext).Assembly);
    }
}