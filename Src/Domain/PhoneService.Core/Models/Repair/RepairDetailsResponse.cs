﻿using PhoneService.Core.Models.Product;
using PhoneService.Core.Models.RepairItem;
using System;
using System.Collections.Generic;
using System.Text;
using PhoneService.Core.Models.Customer;

namespace PhoneService.Core.Models.Repair
{
    public class RepairDetailsResponse
    {
        public int RepairId { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string StatusName { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public CustomerDetailsResponse CustomerDetails { get; set; }
        public ProductResponse Product { get; set; }
        public IEnumerable<RepairItemResponse> RepairItems { get; set; }
    }
}
