﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EstadoResultadoWPF.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EstadoResultadoWPF.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to account_prefix=Cuenta Contable 
        ///
        ///input_A=date
        ///input_B=compte
        ///input_C=type
        ///input_D=comment
        ///input_E=area
        ///input_F=cost_center
        ///input_G=item
        ///input_H=eff_date
        ///input_I=analisys_date
        ///input_J=reference
        ///input_K=ref_date
        ///input_L=exp_date
        ///input_M=debit
        ///input_N=credit
        ///input_O=balance
        ///input_P=branch
        ///
        ///head_status=Estado
        ///head_company=Empresa
        ///head_desc_area=Agrupacion
        ///head_brand=Marca
        ///head_det_eerr=Detalle EERR
        ///head_date=Fecha
        ///head_compte=# Compte
        ///head_type=Tipo
        ///head_comment=Glosa
        ///head_area=Area        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string rcl_format {
            get {
                return ResourceManager.GetString("rcl_format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] rec_config {
            get {
                object obj = ResourceManager.GetObject("rec_config", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
