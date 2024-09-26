using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.TechChallenge.Persistency.Core.Entities;

namespace Postech.TechChallenge.Persistency.Infra.Configurations;

public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.ToTable("tb_contact", "contacts");

        builder.HasKey(c => c.ContactId)
               .HasName("id");

        builder.Property(c => c.ContactId)
               .HasColumnName("id")
               .IsRequired();

        builder.Property(c => c.FirstName)
               .HasColumnName("first_name")
               .IsRequired();

        builder.Property(c => c.LastName)
               .HasColumnName("last_name")
               .IsRequired();

        builder.Property(c => c.Email)
               .HasColumnName("email")
               .IsRequired();

        builder.Property(c => c.ContactPhoneAreaCode)
               .HasColumnName("contact_phone_area_code")
               .IsRequired();

        builder.Property(c => c.ContactPhone)
               .HasColumnName("contact_phone")
               .IsRequired();

        builder.Property(c => c.CreatedAt)
               .HasColumnName("created_at");

        builder.Property(c => c.ModifiedAt)
               .HasColumnName("modified_at");

        builder.Property(c => c.Active)
               .HasColumnName("active");
    }
}
