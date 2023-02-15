using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoping.Application.DTOs
{
    public class SaleDetailCreateDTO
    {

        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quality { get; set; }
        public decimal Total { get; set; }
        public int Sale_Id { get; set; }
    }
}