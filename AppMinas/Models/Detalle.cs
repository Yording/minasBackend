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
    
    public partial class Detalle
    {
        public long idDetalle { get; set; }
        public int idConexion { get; set; }
        public string idActividad { get; set; }
        public string nombreActividad { get; set; }
        public string urlDetalle { get; set; }
        public string descripcion { get; set; }
        public int idTipoDetalle { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
    
        public virtual Conexion Conexion { get; set; }
        public virtual TipoDetalle TipoDetalle { get; set; }
    }
}
