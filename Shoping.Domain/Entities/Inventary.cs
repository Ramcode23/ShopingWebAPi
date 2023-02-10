using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Inventary : BaseEntity
    {
        public int Price { get; set; }
        public int Stock { get; set; }
        //public  ICollection<Product> products { get; set; } = new List<Product>
    }
}
