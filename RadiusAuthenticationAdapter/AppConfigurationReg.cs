using System;
using Microsoft.Win32;

namespace RadiusAuthenticationAdapter
{
    /// <summary>
    /// This derived class implements the AppConfiguration class, providing a registry backend for storing configuration data.
    /// </summary>
    class AppConfigurationReg : AppConfiguration
    {
        public AppConfigurationReg()
        {
            using (RegistryKey appConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Dave Hope\RadiusAuthenticationAdapter", false))
            { 
                if (appConfig != null)
                {
                    _Server = appConfig.GetValue("Server").ToString();

                    if (! uint.TryParse(appConfig.GetValue("AuthenticationPort").ToString(), out _AuthenticationPort))
                    {
                        _AuthenticationPort = 1812;
                    }

                    if (! uint.TryParse(appConfig.GetValue("AccountingPort").ToString(), out _AccountingPort))
                    {
                        _AccountingPort = 1813;
                    }

                    if (! int.TryParse(appConfig.GetValue("TimeOut").ToString(), out _TimeOut))
                    {
                        _TimeOut = 3000;
                    }

                    _SharedSecret = appConfig.GetValue("SharedSecret").ToString();

                    if(! bool.TryParse(appConfig.GetValue("Debug").ToString(), out _Debug))
                    {
                        _Debug = false;
                    }
                }
                else
                {
                    throw new Exception("Configuration data not found.");
                }
            }
        }
    }
}
