﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RadiusAuthenticationAdapter.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RadiusAuthenticationAdapter.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to authenticate - Invalid PIN.
        /// </summary>
        public static string Error_InvalidPIN {
            get {
                return ResourceManager.GetString("Error_InvalidPIN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to authenticate - Chellenge Required.
        /// </summary>
        public static string Error_RADIUS_ACCESS_CHALLENGE {
            get {
                return ResourceManager.GetString("Error_RADIUS_ACCESS_CHALLENGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to authenticate - Service unavailable.
        /// </summary>
        public static string Error_RADIUS_NULL {
            get {
                return ResourceManager.GetString("Error_RADIUS_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to authenticate - Unknown error.
        /// </summary>
        public static string Error_RADIUS_OTHER {
            get {
                return ResourceManager.GetString("Error_RADIUS_OTHER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RADIUS Authentication Adapter.
        /// </summary>
        public static string Metadata_AdminName {
            get {
                return ResourceManager.GetString("Metadata_AdminName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Token PIN Required.
        /// </summary>
        public static string Presentation_PageTitle {
            get {
                return ResourceManager.GetString("Presentation_PageTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Token and PIN.
        /// </summary>
        public static string Presentation_Prompt {
            get {
                return ResourceManager.GetString("Presentation_Prompt", resourceCulture);
            }
        }
    }
}
