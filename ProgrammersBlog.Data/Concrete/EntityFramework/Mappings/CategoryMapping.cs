﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
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
        }
    }
}
