using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities.Comon
{
    public class CancelBaseEntity : BaseEntity
    {
        public DateTime? CanceledAt { get; set; }
        public string? CanceledBy { get; set; }
        public bool IsCanceled { get; set; }
    }
}
