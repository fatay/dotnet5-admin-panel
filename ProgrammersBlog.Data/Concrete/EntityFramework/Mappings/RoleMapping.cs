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
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired(true);
            builder.Property(r => r.Name).HasMaxLength(30);
            builder.Property(r => r.Description).IsRequired(true);
            builder.Property(r => r.Description).HasMaxLength(240);
            builder.Property(r => r.CreatedByName).HasMaxLength(50);
            builder.Property(r => r.CreatedByName).IsRequired(true);
            builder.Property(r => r.ModifiedByName).HasMaxLength(50);
            builder.Property(r => r.ModifiedByName).IsRequired(true);
            builder.Property(r => r.IsActive).IsRequired(true);
            builder.Property(r => r.IsDeleted).IsRequired(true);
            builder.Property(r => r.CreatedDate).IsRequired(true);
            builder.Property(r => r.ModifiedDate).IsRequired(true);
            builder.Property(r => r.Note).IsRequired(true);

            // Tabloyu oluştur.
            builder.ToTable("Roles");

            // Initialize işlemleri : HasData() metodu veritabanında kayıt yoksa eklenmesini sağlar.
            builder.HasData(
            new Role
            {
                Id = 1,
                Name = "Admin",
                Description = "Admin Rolü, tüm yetkilere sahiptir.",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "Admin Rolüdür."
            });
        }
    }
}
