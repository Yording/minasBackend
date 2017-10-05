using AppMinas.Utilidades;
using AppMinas.Listas;
using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using System.Collections.Generic;


namespace AppMinas.Controllers
{
    public class ActividadesController : ApiController
    {

        public Resultado get(string NombreTabla, string Fechainicio, string FechaFin)
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };

            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {

                    objResultado.Data =  objMINASBDEntities.ConsultarActividades(NombreTabla, Fechainicio, FechaFin);

                   

                }
            }
            catch (Exception ex)
            {
                objResultado.TipoResultado = false;
                objResultado.Mensaje = ex.Message;

            }
            return objResultado;
        }




    }
}
