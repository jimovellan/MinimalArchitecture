﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinimalArchitecture.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Error {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Error() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MinimalArchitecture.Common.Resources.Error", typeof(Error).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
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
        ///   Busca una cadena traducida similar a Hubo un error y no se pudo completar la operación..
        /// </summary>
        public static string ERROR_GENERAL {
            get {
                return ResourceManager.GetString("ERROR_GENERAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Login invalido.
        /// </summary>
        public static string LOGIN_INVALID {
            get {
                return ResourceManager.GetString("LOGIN_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Existe un tag ya con ese nombre..
        /// </summary>
        public static string TAG_EXIST_PREVIOUS {
            get {
                return ResourceManager.GetString("TAG_EXIST_PREVIOUS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tag no se pudo eliminar.
        /// </summary>
        public static string TAG_NO_DELETE {
            get {
                return ResourceManager.GetString("TAG_NO_DELETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tag no se encontro..
        /// </summary>
        public static string TAG_NO_FOUND {
            get {
                return ResourceManager.GetString("TAG_NO_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tag no se pudo insertar.
        /// </summary>
        public static string TAG_NO_INSERT {
            get {
                return ResourceManager.GetString("TAG_NO_INSERT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tag no se pudo actualizar.
        /// </summary>
        public static string TAG_NO_UPDATE {
            get {
                return ResourceManager.GetString("TAG_NO_UPDATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El token expiró.
        /// </summary>
        public static string TOKEN_EXPIRED {
            get {
                return ResourceManager.GetString("TOKEN_EXPIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Token no valido.
        /// </summary>
        public static string TOKEN_NOT_VALID {
            get {
                return ResourceManager.GetString("TOKEN_NOT_VALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El usuario no fue encontrado.
        /// </summary>
        public static string USR_NOT_FOUND {
            get {
                return ResourceManager.GetString("USR_NOT_FOUND", resourceCulture);
            }
        }
    }
}
