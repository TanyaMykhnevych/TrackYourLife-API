using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Organ;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    public class OrganInfoService : IOrganInfoService
    {
        private readonly IOrganInfoRepository _organInfoRepository;

        public OrganInfoService(IOrganInfoRepository organInfoRepository)
        {
            _organInfoRepository = organInfoRepository;
        }

        public async Task<IList<OrganInfo>> GetOrganInfosAsync()
        {
            return await _organInfoRepository.GetAllAsync();
        }

        public Task<OrganInfo> GetOrganInfoByIdAsync(int id)
        {
            return _organInfoRepository.GetByIdAsync(id);
        }

        public bool IfOrganInfoExists(int organInfoId)
        {
            return _organInfoRepository.IfOrganInfoExist(organInfoId);
        }

        public Task<OrganInfo> AddOrganInfoAsync(OrganInfo organInfo)
        {
            return _organInfoRepository.AddAsync(organInfo);
        }

        public Task<OrganInfo> UpdateOrganInfoAsync(OrganInfo organInfo)
        {
            return _organInfoRepository.UpdateAsync(organInfo);
        }
    }
}
