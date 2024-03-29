﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities.Organ;

namespace BusinessLayer.Services.Abstractions
{
    public interface ITransplantOrgansService
    {
        TransplantOrgan GetById(int transplantOrganId);

        TransplantOrgan Save(TransplantOrgan transplantOrgan);

        void Update(TransplantOrgan transplantOrgan);
    }
}
