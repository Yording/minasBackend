using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppMinas.Models
{
    public class ConexionModel
    {
        public string idConexion { get; set; }
        public FormularioModel Formulario { get; set; }
        public TipoConexionModel TipoConexion { get; set; }
        public string nombreConexion { get; set; }
        public string fuente { get; set; }
        public string usuarioFuente { get; set; }
        public string contrasenaFuente { get; set; }
        public int periodoSincronizacion { get; set; }
        public string descripcion { get; set; }
        public object fechaActualizacion { get; set; }
    }
}