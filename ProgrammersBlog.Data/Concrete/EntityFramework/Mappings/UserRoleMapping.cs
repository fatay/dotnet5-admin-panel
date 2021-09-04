using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            // User <=> Role matching
            var adminRole = new UserRole
            {
                RoleId = 1,
                UserId = 1
            };

            var editorRole = new UserRole
            {
                RoleId = 2,
                UserId = 2
            };

            // If there is no data in DB, put datas to DB.
            builder.HasData(adminRole, editorRole);
        }
    }
}
