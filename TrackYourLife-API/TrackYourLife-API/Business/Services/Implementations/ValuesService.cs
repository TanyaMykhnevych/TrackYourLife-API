using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourLife.API.Data.Repositories;

namespace TrackYourLife.API.Business.Services.Implementations
{
    public class ValuesService : IValuesService
    {
        private readonly IValuesRepository _valuesRepository;

        public ValuesService(IValuesRepository valuesRepository)
        {
            _valuesRepository = valuesRepository;
        }
    }
}
