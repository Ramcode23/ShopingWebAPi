using Shoping.Domain.Entities.Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class SaleDetail : DetailBaseEntity
    {
        public int Sale_Id { get; set; }
        public Sale Sale { get; set; } = new Sale();
    }
}
