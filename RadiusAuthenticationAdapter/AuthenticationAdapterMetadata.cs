using System;
using System.Collections.Generic;
using Microsoft.IdentityServer.Web.Authentication.External;
using RadiusAuthenticationAdapter.Properties;

namespace RadiusAuthenticationAdapter
{
    class AuthenticationAdapterMetadata : IAuthenticationAdapterMetadata
    {
        /// <summary>
        /// The friendly name of the authentication provider.
        /// </summary>
        public string AdminName
        {
            get { return Resources.Metadata_AdminName; }
        }


        /// <summary>
        /// This should return a list (array) of strings, where each string is
        /// a supported authentication method. If, after successful 
        /// authentication, the TryEndAuthentication method in the 
        /// IAuthenticationAdapter interface return success, this methods must 
        /// contain a claim of type
        /// http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod.
        /// The value of this claim should be one of authentication methods 
        /// listed in the property. In our Authentication Adapter we 
        /// support only one authentication method:
        /// http://schemas.microsoft.com/ws/2012/12/authmethod/otp.
        /// </summary>
        public string[] AuthenticationMethods
        {
            get { return new string[] { "http://schemas.microsoft.com/ws/2012/12/authmethod/otp" }; }
        }


        /// <summary>
        /// List of LCID's the authentication adapter supports.
        /// </summary>
        public int[] AvailableLcids
        {
            get { return new int[] { 1033 }; }
        }


        /// <summary>
        /// List of descriptions for the authentication adapter. Seemingly unused by ADFS.
        /// </summary>
        public Dictionary<int, string> Descriptions
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(1033, "Implements RADIUS based MFA");
                return result;
            }
        }


        /// <summary>
		/// If multiple Authentication Adapters(MFA providers) are available for
		/// a user, a form us presented to the user where he or she can chose how
        /// to perform additional authentication.The strings in this property
        /// (per language) that are used to identify your Authentication Adapter
		/// in that form.
        /// </summary>
        public Dictionary<int, string> FriendlyNames
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(1033, "Use my VPN token");
                return result;
            }
        }


        /// <summary>
        /// This property should contain the claim types that the Authentication 
        /// Adapter requires. These claims, and values, are passed to multiple 
        /// methods in the IAuthenticationAdapter. Only the FIRST one you enter 
        /// here is presented to the adapter; so we will use UPN here.
        /// </summary>
        public string[] IdentityClaims
        {
            get { return new string[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" }; }
        }



        /// <summary>
        /// This is an indication whether or not the Authentication Adapter 
        /// requires an Identity Claim or not. If you require an Identity Claim,
        /// the claim type must be presented through the IdentityClaims property.
        /// </summary>
        public bool RequiresIdentity
        {
            get { return true; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationAdapterMetadata()
        {

        }
    }
}
