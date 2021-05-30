
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
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1200);
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedByName).IsRequired(true);
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired(true);
            builder.Property(c => c.IsActive).IsRequired(true);
            builder.Property(c => c.IsDeleted).IsRequired(true);
            builder.Property(c => c.CreatedDate).IsRequired(true);
            builder.Property(c => c.ModifiedDate).IsRequired(true);
            builder.Property(c => c.Note).IsRequired(true);

            // Article ile Comment arasında 1-n ilişki var. [Bir article birden fazla yorum içerebilir.]
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);

            // Tabloyu oluştur.
            builder.ToTable("Comments");

            // Initialize işlemleri : HasData() metodu veritabanında kayıt yoksa eklenmesini sağlar.
            builder.HasData(
            new Comment { 
                Id = 1,
                ArticleId = 1,
                Text = "Yorum satırı - 1",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "C# Makale Yorumu"
            },
            new Comment {
                Id = 2,
                ArticleId = 2,
                Text = "Yorum satırı - 2",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "C++ Makale Yorumu"
            },
            new Comment {
                Id = 3,
                ArticleId = 3,
                Text = "Yorum satırı - 3",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "JavaScript Makale Yorumu"
            });

        }
    }
}
