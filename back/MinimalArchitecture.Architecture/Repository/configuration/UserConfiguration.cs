using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Repository.configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), "auth");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.Active).HasDefaultValue(true);
            builder.Property(x => x.Hash).IsRequired();
            builder.HasMany(x => x.Tokens);
        }
    }
}
