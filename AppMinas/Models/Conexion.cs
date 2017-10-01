//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppMinas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Conexion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Conexion()
        {
            this.Estructura = new HashSet<Estructura>();
            this.Detalle = new HashSet<Detalle>();
        }
    
        public int idConexion { get; set; }
        public int idTipoConexion { get; set; }
        public int idFormulario { get; set; }
        public string nombreConexion { get; set; }
        public string fuente { get; set; }
        public string usuarioFuente { get; set; }
        public string contrasenaFuente { get; set; }
        public int periodoSincronizacion { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaActualizacion { get; set; }
        public int idJob { get; set; }
    
        public virtual Job Job { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estructura> Estructura { get; set; }
        public virtual Formulario Formulario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle> Detalle { get; set; }
        public virtual TipoConexion TipoConexion { get; set; }
    }
}
