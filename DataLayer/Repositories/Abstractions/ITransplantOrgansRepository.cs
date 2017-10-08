using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Organ;

namespace DataLayer.Repositories.Abstractions
{
    public interface ITransplantOrgansRepository
    {
        void Save(TransplantOrgan transplantOrgan);
    }
}
