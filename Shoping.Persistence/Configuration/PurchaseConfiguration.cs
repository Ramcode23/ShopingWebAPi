using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Persistence.Configuration
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasOne(p => p.Provider)
                .WithMany(p => p.Purchases)
                .HasForeignKey(p => p.Provider_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
