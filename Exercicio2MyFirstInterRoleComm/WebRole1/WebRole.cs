using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // Guiao - 2 - Passo 5.a. Adic. 'a classe Global codigo que tem de correr no inicio da aplic.
            CloudStorageAccount.SetConfigurationSettingPublisher(
                (configName, configSetting) =>
                    {
                        var connectionString = RoleEnvironment.GetConfigurationSettingValue(configName);
                        configSetting(connectionString);
                    });

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
