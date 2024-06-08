using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SphinxCommercial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Data.Configurations
{
    public class ClientProductConfig : IEntityTypeConfiguration<ClientProduct>
    {
        public void Configure(EntityTypeBuilder<ClientProduct> builder)
        {
            builder.HasOne(cp => cp.Client)
                   .WithMany(c => c.ClientProducts)
                   .HasForeignKey(cp => cp.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Product)
                   .WithMany(p => p.ClientProducts)
                   .HasForeignKey(cp => cp.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
