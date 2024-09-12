using Worker.Persistency.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infrastructure.Context;

public class ContactManagementDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }

    public ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options) : base(options)
    {
    }
}
