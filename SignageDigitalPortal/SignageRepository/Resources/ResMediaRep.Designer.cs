﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SignageRepository.Resources {
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
    public class ResMediaRep {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResMediaRep() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SignageRepository.Resources.ResMediaRep", typeof(ResMediaRep).Assembly);
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
        ///   Looks up a localized string similar to El tamaño de la media debe ser menor a 1GB..
        /// </summary>
        public static string ERROR_MEDIA_SIZE_TOOLONG {
            get {
                return ResourceManager.GetString("ERROR_MEDIA_SIZE_TOOLONG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La media que intenta subir no tiene una extensión válida..
        /// </summary>
        public static string ERROR_NOT_MEDIAFILE {
            get {
                return ResourceManager.GetString("ERROR_NOT_MEDIAFILE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sólo es posible subir un archivo a la vez..
        /// </summary>
        public static string ERROR_UPLOAD_NOSINGLEMEDIA {
            get {
                return ResourceManager.GetString("ERROR_UPLOAD_NOSINGLEMEDIA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error desconocido al almacenar la media..
        /// </summary>
        public static string ERROR_UPLOADMEDIA_UNKNOW_ERROR {
            get {
                return ResourceManager.GetString("ERROR_UPLOADMEDIA_UNKNOW_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La media fue almacenada de forma correcta..
        /// </summary>
        public static string INFO_UPLOADMEDIA_SUCCESSFUL {
            get {
                return ResourceManager.GetString("INFO_UPLOADMEDIA_SUCCESSFUL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Externa (páginas Web, videos, imágenes que no se almacenan en disco).
        /// </summary>
        public static string TITLE_MEDIA_EXTERNAL {
            get {
                return ResourceManager.GetString("TITLE_MEDIA_EXTERNAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Local (se almacena en disco el archivo).
        /// </summary>
        public static string TITLE_MEDIA_LOCAL {
            get {
                return ResourceManager.GetString("TITLE_MEDIA_LOCAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Medias.
        /// </summary>
        public static string TITLE_PAGE_CAT {
            get {
                return ResourceManager.GetString("TITLE_PAGE_CAT", resourceCulture);
            }
        }
    }
}
