using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Constants
{
    public static class ConfigurationConstants
    {
        //TODO: replace to 'appsettings.json' and retrieve
        // public const string DbConnection = @"Server=.;Database=TrackYourLife;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public const string DbConnection = 
            @"Server=tcp:mssql-server.database.windows.net,1433;Initial Catalog=TrackYourLife;Persist Security Info=False;User ID=TanyaMy;Password=Test123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
