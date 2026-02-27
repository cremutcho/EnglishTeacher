using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Data.Configurations;

public class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(s => s.Email)
               .IsRequired()
               .HasMaxLength(200);

       builder.HasIndex(s => s.Email)
              .IsUnique();

        builder.Property(s => s.Age)
               .IsRequired();
    }
}