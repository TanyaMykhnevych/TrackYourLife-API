using DataLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContext
{
    public interface IDbInitializer : IDisposable
    {
        Task InitializeAsync();
    }
}
