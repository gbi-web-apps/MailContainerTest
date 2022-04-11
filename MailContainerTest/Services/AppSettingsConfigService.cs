using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class AppSettingsConfigService : IConfigService
    {
        public bool IsBackupStore()
        {
            return ConfigurationManager.AppSettings["DataStoreType"] == "Backup";
        }
    }
}
