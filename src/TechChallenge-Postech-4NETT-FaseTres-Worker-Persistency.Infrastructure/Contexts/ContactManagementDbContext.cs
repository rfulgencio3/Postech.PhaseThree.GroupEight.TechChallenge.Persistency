using Microsoft.EntityFrameworkCore;
using Postech.TechChallenge.Persistency.Core.Entities;

namespace Postech.TechChallenge.Persistency.Infra.Contexts
{
    public class ContactManagementDbContext : DbContext
    {
        public ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactEntity> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactManagementDbContext).Assembly);

            modelBuilder.Entity<ContactEntity>()
                .ToTable("tb_contact", "contacts");
        }
    }
}
