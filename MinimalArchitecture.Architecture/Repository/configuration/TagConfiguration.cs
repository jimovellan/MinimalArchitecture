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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag),"post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                    .HasMaxLength(25)
                    .IsRequired();
        }
    }
}
