using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using System.Collections.Generic;
using Common.Entities.Organ;

namespace BusinessLayer.Services.Implementations
{
    public class OrganInfoService : IOrganInfoService
    {
        private readonly IOrganInfoRepository _organInfoRepository;

        public OrganInfoService(IOrganInfoRepository organInfoRepository)
        {
            _organInfoRepository = organInfoRepository;
        }

        public IList<OrganInfo> GetOrganInfos()
        {
            return _organInfoRepository.GetAll();
        }

        public OrganInfo GetOrganInfoById(int id)
        {
            return _organInfoRepository.GetById(id);
        }

        public bool IfOrganInfoExists(int organInfoId)
        {
            return _organInfoRepository.IfOrganInfoExist(organInfoId);
        }

        public OrganInfo AddOrganInfo(OrganInfo organInfo)
        {
            return _organInfoRepository.Add(organInfo);
        }

        public OrganInfo UpdateOrganInfo(OrganInfo organInfo)
        {
            return _organInfoRepository.Update(organInfo);
        }
    }
}
