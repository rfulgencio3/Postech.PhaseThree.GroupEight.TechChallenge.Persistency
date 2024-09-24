using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Configurations;

public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.ToTable("tb_contact", "contacts");

        builder.HasKey(c => c.ContactId)
               .HasName("contact_id");

        builder.Property(c => c.ContactId)
               .HasColumnName("contact_id")
               .IsRequired();

        builder.Property(c => c.FirstName)
               .HasColumnName("contact_first_name")
               .IsRequired();

        builder.Property(c => c.LastName)
               .HasColumnName("contact_last_name")
               .IsRequired();

        builder.Property(c => c.Email)
               .HasColumnName("contact_email")
               .IsRequired();

        builder.Property(c => c.ContactPhone)
               .HasColumnName("contact_contact_phone_id")
               .IsRequired();

        builder.Property(c => c.CreatedAt)
               .HasColumnName("contact_created_at");

        builder.Property(c => c.ModifiedAt)
               .HasColumnName("contact_modified_at");

        builder.Property(c => c.Active)
               .HasColumnName("contact_active");
    }
}
