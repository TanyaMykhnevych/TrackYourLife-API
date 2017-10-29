using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DbContext
{
    public interface IDbInitializer
    {
        void Initialize();
    }
}
