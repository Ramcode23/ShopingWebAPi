using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Provider_Id { get; set; }
        public Provider Provider { get; set; } = new Provider();
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new  List<PurchaseDetail>();

        public DateTime? CanceledAt { get; set; }
        public string? CanceledBy { get; set; }
        public bool IsCanceled { get; set; }
    }
}
