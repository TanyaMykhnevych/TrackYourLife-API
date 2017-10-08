using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Organ;

namespace BusinessLayer.Services.Abstractions
{
    public interface ITransplantOrgansService
    {
        void Save(TransplantOrgan transplantOrgan);
    }
}
