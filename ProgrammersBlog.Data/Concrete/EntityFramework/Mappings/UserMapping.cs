using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Email).IsRequired(true);
            builder.Property(u => u.Email).HasMaxLength(60);
            builder.HasIndex(u => u.Email).IsUnique(true);
            builder.Property(u => u.Username).IsRequired(true);
            builder.Property(u => u.Username).HasMaxLength(24);
            builder.HasIndex(u => u.Username).IsUnique(true);
            builder.Property(u => u.PasswordHash).IsRequired(true);
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(480)");
            builder.Property(u => u.Description).HasMaxLength(480);
            builder.Property(u => u.FirstName).IsRequired(true);
            builder.Property(u => u.FirstName).HasMaxLength(32);
            builder.Property(u => u.LastName).IsRequired(true);
            builder.Property(u => u.LastName).HasMaxLength(32);
            builder.Property(u => u.Picture).IsRequired(true);
            builder.Property(u => u.Picture).HasMaxLength(240);
            builder.Property(u => u.CreatedByName).HasMaxLength(50);
            builder.Property(u => u.CreatedByName).IsRequired(true);
            builder.Property(u => u.ModifiedByName).HasMaxLength(50);
            builder.Property(u => u.ModifiedByName).IsRequired(true);
            builder.Property(u => u.IsActive).IsRequired(true);
            builder.Property(u => u.IsDeleted).IsRequired(true);
            builder.Property(u => u.CreatedDate).IsRequired(true);
            builder.Property(u => u.ModifiedDate).IsRequired(true);
            builder.Property(u => u.Note).IsRequired(true);

            // Role ile User arasında 1 - n ilişki olmalıdır.
            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            // Tabloyu oluştur.
            builder.ToTable("Users");
        }
    }
}
