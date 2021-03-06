﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneService.Core.Services.Healpers;
using PhoneService.Domain;
using PhoneService.Domain.Repository;
using PhoneService.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneService.Core.Repository
{
    public class RepairRepository : IRepairRepository
    {
        protected readonly PhoneServiceDbContext _context;
        protected readonly SearchFilterHealpers _searchFilterHealpers;

        public RepairRepository(PhoneServiceDbContext context, SearchFilterHealpers searchFilterHealpers)
        {
            _context = context;
            _searchFilterHealpers = searchFilterHealpers;
        }

        public async Task<IEnumerable<Repair>> GetAllRepairsAsync()
        {
            var repairs = await _context.Set<Repair>()
                            .Include(c => c.Product)
                            .Include(c => c.RepairStatus)
                            .Include(c => c.Customer)
                            .OrderByDescending(c => c.CreateDate)
                            .ToListAsync();

            return repairs;
        }

        public async Task<Repair> GetRepairItemByIdAsync(int repairId)
        {
            var repair = await _context.Set<Repair>()
                            .Include(c => c.Customer)
                                    .ThenInclude(c => c.Addres)
                            .Include(c => c.Product)
                                    .ThenInclude(c => c.ProductSapareParts)
                                            .ThenInclude(c => c.SaparePart)
                            .Include(c => c.RepairItems)
                                    .ThenInclude(c => c.SaparePart)
                            .Include(c => c.RepairStatus)
                            .OrderByDescending(c => c.CreateDate)
                                    .FirstOrDefaultAsync(c => c.RepairId == repairId);

            return repair;
        }

        public async Task<IEnumerable<Repair>> GetRepairBySearchTermsAsync(DateTime? dateFrom, DateTime? dateTo, Repair repairRequest)
        {
            IEnumerable<Repair> repairs = _context.Set<Repair>()
                                .Include(c => c.Customer)
                                    .ThenInclude(c => c.Addres)
                                .Include(c => c.Product)
                                    .ThenInclude(c => c.ProductSapareParts)
                                .Include(c => c.RepairItems)
                                    .ThenInclude(c => c.SaparePart)
                                .Include(c => c.RepairStatus)
                                    .AsQueryable()
                                .OrderByDescending(c => c.CreateDate);

            var searchResponse = await _searchFilterHealpers.SearchByContainsWithDateAsync(dateFrom, dateTo, repairs, repairRequest);
            var response = Mapper.Map<IEnumerable<Repair>>(searchResponse);

            return response;
        }

        public async Task<IEnumerable<Repair>> GetRepairBySearchDateAsync(DateTime? dateFrom, DateTime? dateTo, Repair repairRequest)
        {
            IEnumerable<Repair> repairs = _context.Set<Repair>()
                .Include(c => c.Customer)
                .ThenInclude(c => c.Addres)
                .Include(c => c.Product)
                .ThenInclude(c => c.ProductSapareParts)
                .Include(c => c.RepairItems)
                .ThenInclude(c => c.SaparePart)
                .Include(c => c.RepairStatus)
                .AsQueryable()
                .OrderByDescending(c => c.CreateDate);

            var searchResponse = await _searchFilterHealpers.SearchByContainsWithDateAsync(dateFrom, dateTo, repairs, repairRequest);
            var response = Mapper.Map<IEnumerable<Repair>>(searchResponse);

            return response;
        }

        public void AddRepair(Repair repairItem) => _context.Add(repairItem);

        public void UpdateRepair(Repair repairItem) => _context.Update(repairItem);

        public void RemoveRepair(Repair repairItem) => _context.Remove(repairItem);

        #region Repair Status Count

        public async Task<int> GetNewRepairStatusCountAsync() => await _context.Repairs.CountAsync(r => r.RepairStatusId == 1);
        public async Task<int> GetPricedRepairStatusCountAsync() => await _context.Repairs.CountAsync(r => r.RepairStatusId == 2);
        public async Task<int> GetInProgressRepairStatusCountAsync() => await _context.Repairs.CountAsync(r => r.RepairStatusId == 3);
        public async Task<int> GetCompletedRepairStatusCountAsync() => await _context.Repairs.CountAsync(r => r.RepairStatusId == 4 || r.RepairStatusId == 5);
        public async Task<int> GetRejectedRepairStatusCountAsync() => await _context.Repairs.CountAsync(r => r.RepairStatusId == 6);

        #endregion
    }
}
