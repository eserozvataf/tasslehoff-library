﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tasslehoff.Library.Services {
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
    internal class LocalResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Tasslehoff.Library.Services.LocalResource", typeof(LocalResource).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while starting service &apos;{0}&apos;..
        /// </summary>
        internal static string AnErrorOccurredWhileStartingService {
            get {
                return ResourceManager.GetString("AnErrorOccurredWhileStartingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while stopping service &apos;{0}&apos;..
        /// </summary>
        internal static string AnErrorOccurredWhileStoppingService {
            get {
                return ResourceManager.GetString("AnErrorOccurredWhileStoppingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service &apos;{0}&apos; has been started..
        /// </summary>
        internal static string ServiceHasBeenStarted {
            get {
                return ResourceManager.GetString("ServiceHasBeenStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service &apos;{0}&apos; has been stopped..
        /// </summary>
        internal static string ServiceHasBeenStopped {
            get {
                return ResourceManager.GetString("ServiceHasBeenStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service &apos;{0}&apos; is being started..
        /// </summary>
        internal static string ServiceIsBeingStarted {
            get {
                return ResourceManager.GetString("ServiceIsBeingStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service &apos;{0}&apos; is being stopped..
        /// </summary>
        internal static string ServiceIsBeingStopped {
            get {
                return ResourceManager.GetString("ServiceIsBeingStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service is not ready to get started..
        /// </summary>
        internal static string TheServiceIsNotReadyToGetStarted {
            get {
                return ResourceManager.GetString("TheServiceIsNotReadyToGetStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service is not ready to get stopped..
        /// </summary>
        internal static string TheServiceIsNotReadyToGetStopped {
            get {
                return ResourceManager.GetString("TheServiceIsNotReadyToGetStopped", resourceCulture);
            }
        }
    }
}
