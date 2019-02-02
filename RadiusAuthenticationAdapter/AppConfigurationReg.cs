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
                    // RADIUS Server - Required value.
                    var regServer = appConfig.GetValue("Server");
                    if (regServer != null)
                    {
                        _Server = regServer.ToString();
                    }
                    else
                    {
                        Logging.LogMessage("Configuration data invalid - Missing Server");
                        throw new Exception("Configuration data not found (Server).");
                    }


                    // RADIUS Authentication Port - Optional value.
                    var regAuthenticationPort = appConfig.GetValue("AuthenticationPort");
                    if (regAuthenticationPort != null)
                    {
                        if (!uint.TryParse(regAuthenticationPort.ToString(), out _AuthenticationPort))
                        {
                            _AuthenticationPort = 1812;
                        }
                    }


                    // RADIUS Accounting Port - Optional value.
                    var regAccountingPort = appConfig.GetValue("AccountingPort");
                    if (regAccountingPort != null)
                    {
                        if (!uint.TryParse(regAccountingPort.ToString(), out _AccountingPort))
                        {
                            _AccountingPort = 1813;
                        }
                    }


                    // RADIUS Timeout - Optional value.
                    var regTimeOut = appConfig.GetValue("TimeOut");
                    if (regTimeOut != null)
                    {
                        if (!int.TryParse(regTimeOut.ToString(), out _TimeOut))
                        {
                            _TimeOut = 3000;
                        }
                    }
                    else
                    {
                        _TimeOut = 3000;
                    }


                    // RADIUS Shared Secret - Required value.
                    var regSharedSecret = appConfig.GetValue("SharedSecret");
                    if( regSharedSecret != null )
                    {
                        _SharedSecret = regSharedSecret.ToString();
                    }
                    else
                    {
                        Logging.LogMessage("Configuration data invalid - Missing SharedSecret");
                        throw new Exception("Configuration data not found.");
                    }


                    // Debug Setting - Optional value.
                    var regDebug = appConfig.GetValue("Debug");
                    if( regDebug != null )
                    {
                        if (!bool.TryParse(regDebug.ToString(), out _Debug))
                        {
                            _Debug = false;
                        }
                    }
                    else
                    {
                        _Debug = false;
                    }

                    var regIdentityClaims = appConfig.GetValue("IdentityClaims");
                    if (regIdentityClaims != null)
                    {
                        _IdentityClaims = regIdentityClaims.ToString();
                    }
                    else
                    {
                        Logging.LogMessage("Configuration data invalid - Missing IdentityClaims");
                        throw new Exception("Configuration data not found (IdentityClaims).");
                    }

                    var regNasAddress = appConfig.GetValue("NasAddress");
                    if (regNasAddress != null)
                    {
                        _NasAddress = regNasAddress.ToString();
                    }
                    else
                    {
                        _NasAddress = String.Empty;
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
