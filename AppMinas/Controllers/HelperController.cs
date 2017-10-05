using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppMinas.Models;
using AppMinas.Listas;

namespace AppMinas.Controllers
{
    public class HelperController : ApiController
    {

        // Consulta las conexiones disponibles
        public Resultado get(int idJob)
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };

            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {

                    List<Models.ConexionesDisponiblesSincronizarConsultar_Result> lstConexionesDisponible = objMINASBDEntities.ConexionesDisponiblesSincronizarConsultar(idJob).ToList();

                    objResultado.Data = lstConexionesDisponible;

                }
            }
            catch (Exception ex)
            {
                objResultado.TipoResultado = false;
                objResultado.Mensaje = ex.Message;

            }
            return objResultado;
        }

        //Actualiza la fecha de actualizacion de una conexion cuando esta se ha sincronizado
        public Resultado Post(string GuidFormulario)
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };
            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {
                    objMINASBDEntities.ActualizarFechaConexion(GuidFormulario);
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
