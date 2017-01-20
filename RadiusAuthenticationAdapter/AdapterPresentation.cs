using System;
using Microsoft.IdentityServer.Web.Authentication.External;
using RadiusAuthenticationAdapter.Properties;

namespace RadiusAuthenticationAdapter
{
    class AdapterPresentation : IAdapterPresentation, IAdapterPresentationForm
    {
        private string message;
        private bool isPermanentFailure;

        /// <summary>
        /// Used by AD FS to query to Authentication Adapter for the title of the authentication page.
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public string GetPageTitle(int lcid)
        {
            return Resources.Presentation_PageTitle;
        }


        /// <summary>
        /// Used to represent the HTML code that is inserted in the AD FS sing-in page.
        /// </summary>
        /// <param name="lcid">Language Code Identifier</param>
        /// <returns>HTML to be inserted</returns>
        public string GetFormHtml(int lcid)
        {
            string result = "";
            if (!String.IsNullOrEmpty(this.message))
            {
                result += "<p style=\"color:red\">" + message + "</p><p>&nbsp;</p>";
            }
            if (!this.isPermanentFailure)
            {
                result += "<form method=\"post\" id=\"loginForm\" autocomplete=\"off\">";
                result += Resources.Presentation_Prompt + ": <input id=\"pin\" name=\"pin\" type=\"password\" />";
                result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
                result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
                result += "<input id=\"continueButton\" type=\"submit\" name=\"Continue\" value=\"Continue\" />";
                result += "</form>";
            }
            return result;
        }


        /// <summary>
        /// Used to allow the Authentication Adapter to insert any special tags etc. in the <head> element of the AD FS sign-in page.
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public string GetFormPreRenderHtml(int lcid)
        {
            return string.Empty;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public AdapterPresentation()
        {
            this.message = string.Empty;
            this.isPermanentFailure = false;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Erorr message to display</param>
        /// <param name="isPermanentFailure"></param>
        public AdapterPresentation(string message, bool isPermanentFailure)
        {
            this.message = message;
            this.isPermanentFailure = isPermanentFailure;
        }
    }
}
