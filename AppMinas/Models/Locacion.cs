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
    
    public partial class Locacion
    {
        public long idLocacion { get; set; }
        public string GUIDLocation { get; set; }
        public string nombre { get; set; }
        public string nombreContacto { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string fax { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string departamento { get; set; }
        public string pais { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string tagUID { get; set; }
        public int idTipoLocacion { get; set; }
        public System.DateTime fechaActualizacion { get; set; }
    
        public virtual TipoLocacion TipoLocacion { get; set; }
    }
}
