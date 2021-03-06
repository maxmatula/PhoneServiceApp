﻿using AutoMapper;
using PhoneService.Core.Interfaces;
using PhoneService.Core.Mapping;
using PhoneService.Core.Models.Other;
using PhoneService.Core.Models.Repair;
using PhoneService.Core.Models.RepairItem;
using PhoneService.Domain;
using PhoneService.Domain.Repository.IUnitOfWork;
using PhoneService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneService.Core.Services
{
    public class RepairService : IRepairService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NullCheckMethod _nullCheckMethod;
        private readonly IEmailService _emailService;
        private readonly RepairMappingProfile _repairMappingProfile;

        public RepairService(IUnitOfWork unitOfWork, NullCheckMethod nullCheckMethod, IEmailService emailService, RepairMappingProfile repairMappingProfile)
        {
            _unitOfWork = unitOfWork;
            _nullCheckMethod = nullCheckMethod;
            _emailService = emailService;
            _repairMappingProfile = repairMappingProfile;
        }


        public async Task<IEnumerable<RepairResponse>> GetAllRepairsAsync()
        {
            var repairs = await _unitOfWork.Repairs.GetAllRepairsAsync();

            _nullCheckMethod.CheckIfResponseListIsEmpty(repairs);

            var repairsResponse = Mapper.Map<IEnumerable<RepairResponse>>(repairs);

            return repairsResponse;
        }

        public async Task<RepairDetailsResponse> GetRepairByIdAsync(int repairId)
        {
            _nullCheckMethod.CheckIfRequestIsNull(repairId);

            var repair = await _unitOfWork.Repairs.GetRepairItemByIdAsync(repairId);

            _nullCheckMethod.CheckIfResponseIsNull(repair);

            var repairResponse = Mapper.Map<RepairDetailsResponse>(repair);

            return repairResponse;
        }

        public async Task<IEnumerable<RepairResponse>> GetRepairBySearchTermsAsync(RepairSearchRequest searchRequest)
        {
            var searchFilter = new Repair();

            searchFilter = _repairMappingProfile.ConvertRepairSearchRequestToRepair(searchRequest, searchFilter);

            var repair = await _unitOfWork.Repairs.GetRepairBySearchTermsAsync(searchRequest.DateFrom, searchRequest.DateTo, searchFilter);

            _nullCheckMethod.CheckIfResponseListIsEmpty(repair);

            var response = Mapper.Map<IEnumerable<RepairResponse>>(repair);

            return response;
        }

        public async Task AddRepairAsync(RepairAddRequest repairAddRequest)
        {
            _nullCheckMethod.CheckIfRequestIsNull(repairAddRequest);

            var _repair = Mapper.Map<Repair>(repairAddRequest);

            _unitOfWork.Repairs.AddRepair(_repair);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateRepairAsync(RepairUpdateRequest repairUpdateRequest)
        {
            _nullCheckMethod.CheckIfRequestIsNull(repairUpdateRequest);

            var repair = await _unitOfWork.Repairs.GetRepairItemByIdAsync(repairUpdateRequest.RepairId);

            _nullCheckMethod.CheckIfResponseIsNull(repair);

            repair = _repairMappingProfile.ConvertRepairAddRequestToRepair(repairUpdateRequest, repair);

            if (repair.RepairStatusId == 1)
                if (repair.RepairItems != null)
                    repair.RepairStatusId = 2;
            
            await _unitOfWork.CompleteAsync();

            await _emailService.SendRepairStatusNotifyEmailAsync(repairUpdateRequest.RepairId, repairUpdateRequest.Links);
        }

        public async Task UpdateRepairStatusAsync(RepairStatusUpdateRequest repairStatusUpdateRequest)
        {
            _nullCheckMethod.CheckIfRequestIsNull(repairStatusUpdateRequest);

            var repair = await _unitOfWork.Repairs.GetRepairItemByIdAsync(repairStatusUpdateRequest.RepairId);

            _nullCheckMethod.CheckIfResponseIsNull(repair);

            repair.RepairStatusId = repairStatusUpdateRequest.RepairStatusId;

            await _unitOfWork.CompleteAsync();

            await _emailService.SendRepairStatusNotifyEmailAsync(repairStatusUpdateRequest.RepairId, repairStatusUpdateRequest.Links);
        }

        public async Task RemoveRepairAsync(int repairId)
        {
            _nullCheckMethod.CheckIfRequestIsNull(repairId);

            var repair = await _unitOfWork.Repairs.GetRepairItemByIdAsync(repairId);
            var repairItem = await _unitOfWork.RepairItems.GetAllRepairItemByRepairIdAsync(repairId);

            _unitOfWork.RepairItems.RemoveRangeRepairItems(repairItem);
            _unitOfWork.Repairs.RemoveRepair(repair);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Statistics> GetRepairStatusCountAsync()
        {
            var statisticsData = new Statistics();

            statisticsData.New = await _unitOfWork.Repairs.GetNewRepairStatusCountAsync();
            statisticsData.Priced = await _unitOfWork.Repairs.GetPricedRepairStatusCountAsync();
            statisticsData.InProgress = await _unitOfWork.Repairs.GetInProgressRepairStatusCountAsync();
            statisticsData.Completed = await _unitOfWork.Repairs.GetCompletedRepairStatusCountAsync();
            statisticsData.Rejected = await _unitOfWork.Repairs.GetRejectedRepairStatusCountAsync();

            return statisticsData;
        }

        public async Task<Statistics> GetRepairStatusCountBySearchAsync(RepairSearchRequest searchStatistics)
        {
            var searchFilter = new Repair();

            searchFilter = _repairMappingProfile.ConvertRepairSearchRequestToRepair(searchStatistics, searchFilter);

            var repair = await _unitOfWork.Repairs.GetRepairBySearchDateAsync(searchStatistics.DateFrom, searchStatistics.DateTo, searchFilter);

            var statisticsData = new Statistics();

            statisticsData.New = repair.Count(x => x.RepairStatusId == 1);
            statisticsData.Priced = repair.Count(x => x.RepairStatusId == 2);
            statisticsData.InProgress = repair.Count(x => x.RepairStatusId == 3);
            statisticsData.Completed = repair.Count(x => x.RepairStatusId == 5);
            statisticsData.Rejected = repair.Count(x => x.RepairStatusId == 7);

            return statisticsData;
        }
    }
}
