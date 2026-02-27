using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Data.Configurations;

public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(t => t.Email)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(t => t.Subject)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(t => t.Email)
               .IsUnique();

        // Filtro global: apenas professores ativos
        builder.HasQueryFilter(t => t.IsActive);
    }
}