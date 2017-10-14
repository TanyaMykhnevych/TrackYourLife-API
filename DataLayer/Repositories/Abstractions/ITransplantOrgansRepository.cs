using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Organ;

namespace DataLayer.Repositories.Abstractions
{
    public interface ITransplantOrgansRepository
    {
        TransplantOrgan GetById(int transplantOrganId);

        TransplantOrgan Save(TransplantOrgan transplantOrgan);

        void Update(TransplantOrgan transplantOrgan);
    }
}
