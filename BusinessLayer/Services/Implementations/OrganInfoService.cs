using BusinessLayer.Services.Abstractions;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class OrganInfoService : IOrganInfoService
    {
        private readonly IOrganInfoRepository _organInfoRepository;

        public OrganInfoService(IOrganInfoRepository organInfoRepository)
        {
            _organInfoRepository = organInfoRepository;
        }

        public bool IfOrganInfoExists(int organInfoId)
        {
            return _organInfoRepository.IfOrganInfoExist(organInfoId);
        }
    }
}
