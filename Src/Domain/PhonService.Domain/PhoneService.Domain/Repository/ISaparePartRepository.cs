﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneService.Domain.Repository
{
    public interface ISaparePartRepository
    {
        Task<IEnumerable<SaparePart>> GetAllSaparePartByProductIdAsync(int productId);

        Task<SaparePart> GetSaparePartByIdAsync(int saparePartId);

        Task<ProductSaparePart> GetProductSaparePartByIdAsync(int saparePartId, int productId);

        Task<SaparePart> GetLatestSaparePartAsync();

        Task<bool> CheckIfIsUseInRepair(int saparePartId);

        void AddSaparePart(SaparePart saparePart);

        void RemoveSaparePart(SaparePart saparePart);
    }
}
