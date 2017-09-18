using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppMinas.Models
{
    public class FormularioModel
    {
        public int idFormulario { get; set; }
        public string GUIDFormulario { get; set; }
        public string nombreFormulario { get; set; }
        public bool estado { get; set; }
    }
}