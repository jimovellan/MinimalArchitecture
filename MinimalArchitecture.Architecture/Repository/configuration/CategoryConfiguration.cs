using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Repository.configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category), "post");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
