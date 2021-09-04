using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class CategoryMapping : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(60);
            builder.Property(c => c.Description).HasMaxLength(480);
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedByName).IsRequired(true);
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired(true);
            builder.Property(c => c.IsActive).IsRequired(true);
            builder.Property(c => c.IsDeleted).IsRequired(true);
            builder.Property(c => c.CreatedDate).IsRequired(true);
            builder.Property(c => c.ModifiedDate).IsRequired(true);
            builder.Property(c => c.Note).IsRequired(true);
            builder.ToTable("Categories");

            // Initialize işlemleri : HasData() metodu veritabanında kayıt yoksa eklenmesini sağlar.
            builder.HasData(
            new Category
            {
                Id = 1,
                Name = "C#",
                Description = "C# Programlama Dili İle İlgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "C# Blog Kategorisi"
            },
            new Category
            {
                Id = 2,
                Name = "C++",
                Description = "C++ Programlama Dili İle İlgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "C++ Blog Kategorisi"
            },
            new Category
            {
                Id = 3,
                Name = "JavaScript",
                Description = "JavaScript Programlama Dili İle İlgili En Güncel Bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialModify",
                ModifiedDate = DateTime.Now,
                Note = "JavaScript Blog Kategorisi"
            });
        }
    }
}
