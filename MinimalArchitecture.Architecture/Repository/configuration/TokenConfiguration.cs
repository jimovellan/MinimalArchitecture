using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalArchitecture.Entities.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Repository.configuration
{
    internal class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User);
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x=>x.AsociatedToken).IsRequired();
            builder.Property(x=>x.RefreshToken).IsRequired();
            builder.Property(x=>x.ExpirationTime).IsRequired();
        }
    }
}
