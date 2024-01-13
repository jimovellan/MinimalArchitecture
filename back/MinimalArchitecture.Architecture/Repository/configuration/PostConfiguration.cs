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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(nameof(Post), "post");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Owner).IsRequired();
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Html).IsRequired();
            builder.HasMany(x => x.Tags);
            
           
        }
    }
}
