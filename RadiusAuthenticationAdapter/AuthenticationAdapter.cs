using System;
using Microsoft.IdentityServer.Web.Authentication.External;
using RadiusAuthenticationAdapter.Properties;
using FP.Radius;

namespace RadiusAuthenticationAdapter
{
    class AuthenticationAdapter : IAuthenticationAdapter
    {
        private RadiusClient radiusClient;
        private string userPrincipalName;
        private Boolean debugLogging = false;


        /// <summary>
        /// Called once AD FS decides that MFA is required for a user.
        /// </summary>
        /// <param name="identityClaim"></param>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IAdapterPresentation BeginAuthentication(System.Security.Claims.Claim identityClaim, System.Net.HttpListenerRequest request, IAuthenticationContext context)
        {
            // This is needed so we can access the UPN in TryEndAuthentication().
            this.userPrincipalName = identityClaim.Value;

            return new AdapterPresentation();
        }


        /// <summary>
        /// Determines if the Authentication Adapter can perform MFA for the
        /// user. Not currently implemented.
        /// </summary>
        /// <param name="identityClaim"></param>
        /// <param name="context"></param>
        /// <returns>True</returns>
        public bool IsAvailableForUser(System.Security.Claims.Claim identityClaim, IAuthenticationContext context)
        {
            // TODO: Consider checking AD Group Membership before proceeding.
            return true;
        }


        /// <summary>
        /// Used by ADFS to learn about this Authentication Provider.
        /// </summary>
        public IAuthenticationAdapterMetadata Metadata
        {
            get { return new AuthenticationAdapterMetadata(); }
        }


        /// <summary>
        /// Called whenever the Authentication Provider is loaded into the
        /// AD FS pipeline
        /// </summary>
        /// <param name="configData"></param>
        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            var appConfig = new AppConfigurationReg();
            radiusClient = new RadiusClient(appConfig.Server, appConfig.SharedSecret, appConfig.TimeOut,
               appConfig.AuthenticationPort, appConfig.AccountingPort);

            debugLogging = appConfig.Debug;
            if(this.debugLogging)
            {
                Logging.LogMessage(
                    "Currently using the following configuration:" + Environment.NewLine +
                    "Server: " + appConfig.Server + Environment.NewLine +
                    "Authentication port: " + appConfig.AuthenticationPort + Environment.NewLine +
                    "Accounting port: " + appConfig.AccountingPort + Environment.NewLine +
                    "Shared secret: " + appConfig.SharedSecret + Environment.NewLine +
                    "Timeout: " + appConfig.TimeOut);
            }
        }


        /// <summary>
        /// Called whenever the Authentication Provider is unloaded by the
        /// AD FS pipeline.
        /// </summary>
        public void OnAuthenticationPipelineUnload()
        {
        }


        /// <summary>
        /// Called whenever something goes wrong in either the BeginAuthentication
        /// TryEndAuthentication methods. Calls AdapterPresentation to display
        /// a nice message to the end user.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public IAdapterPresentation OnError(System.Net.HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            Logging.LogMessage(
                "An error occured authenticating the user." +
                "Username: " + this.userPrincipalName + Environment.NewLine +
                "Error: " + ex.Message);

            return new AdapterPresentation(ex.Message, true);
        }


        /// <summary>
        /// Called by AD FS to perform the actual authentication.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="proofData"></param>
        /// <param name="request"></param>
        /// <param name="claims"></param>
        /// <returns> If the Authentication Adapter has successfully performed 
        /// the authentication a claim of type 
        /// http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod
        /// is returned
        /// </returns>
        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext context, IProofData proofData, System.Net.HttpListenerRequest request, out System.Security.Claims.Claim[] claims)
        {
            claims = null;
            IAdapterPresentation result = null;

            // Ensure the submitted form isn't empty.
            if (proofData == null || proofData.Properties == null || !proofData.Properties.ContainsKey("pin"))
            {
                if (this.debugLogging)
                {
                    Logging.LogMessage("Either proffData is null or does not contain required property");
                }
                throw new ExternalAuthenticationException(Resources.Error_InvalidPIN, context);
            }
            string pin = proofData.Properties["pin"].ToString();

            // Construct RADIUS auth request.
            var authPacket = radiusClient.Authenticate(this.userPrincipalName, pin);
            var receivedPacket = radiusClient.SendAndReceivePacket(authPacket).Result;

            // Handle no response from RADIUS server.
            if (receivedPacket == null)
            {
                if(this.debugLogging)
                {
                    Logging.LogMessage("No response received from RADIUS server.");
                }
                throw new ExternalAuthenticationException(Resources.Error_RADIUS_NULL, context);
            }

            // Examine the different RADIUS responses
            switch (receivedPacket.PacketType)
            {
                case RadiusCode.ACCESS_ACCEPT:
                    System.Security.Claims.Claim claim = new System.Security.Claims.Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2012/12/authmethod/otp");
                    claims = new System.Security.Claims.Claim[] { claim };
                    break;
                case RadiusCode.ACCESS_CHALLENGE:
                    // No way to cater for this. Fail.
                    result = new AdapterPresentation(Resources.Error_RADIUS_ACCESS_CHALLENGE, false);
                    break;
                case RadiusCode.ACCESS_REJECT:
                    result = new AdapterPresentation(Resources.Error_InvalidPIN, false);
                    break;
                default:
                    result = new AdapterPresentation(Resources.Error_RADIUS_OTHER, false);
                    break;
            }

            if (this.debugLogging)
            {
                Logging.LogMessage(
                    "Processed authentication response." + Environment.NewLine +
                    "Packet Type: " + receivedPacket.PacketType.ToString() + Environment.NewLine +
                    "User: " + this.userPrincipalName );
            }

            return result;
        }
    }
}
