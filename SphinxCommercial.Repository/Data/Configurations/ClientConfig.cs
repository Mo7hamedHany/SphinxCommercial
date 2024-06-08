using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SphinxCommercial.Core.Models;

namespace SphinxCommercial.Repository.Data.Configurations
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Class)
                   .HasConversion<string>();

            builder.Property(c => c.State)
                   .HasConversion<string>();

            builder.HasIndex(c => c.Code)
                   .IsUnique();

        }
    }
}
