using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        //public ICollection<User> IdUser { get; set; } = new List<User>();

        public ICollection<DeliveryDetail> DeliveryDetailId { get; set;} = new List<DeliveryDetail>();
        //public ICollection<Sale> SaleId { get; set;}
    }
}
