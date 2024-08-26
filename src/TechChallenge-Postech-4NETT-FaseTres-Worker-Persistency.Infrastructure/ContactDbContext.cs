using Worker.Persistency.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infrastructure;

public class ContactDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
    {
    }
}
