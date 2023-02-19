﻿using Shoping.Domain.Entities.Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Sale : CancelBaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Client_Id { get; set; }
        // public Client Client { get; set; } = new Client();
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
