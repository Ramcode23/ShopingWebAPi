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
    public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.HasOne(pd => pd.Purchase)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.Purchase_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
