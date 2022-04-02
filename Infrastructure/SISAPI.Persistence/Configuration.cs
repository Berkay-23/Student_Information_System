using Microsoft.Extensions.Configuration;
using System.IO;

namespace SIS.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/SISAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("SIS");
            } 
        }
    }
}
