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
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.Sale_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
