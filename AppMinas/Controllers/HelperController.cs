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
        public Resultado get()
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };

            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {

                    List<Models.ConexionesDisponiblesSincronizarConsultar_Result> lstConexionesDisponible = objMINASBDEntities.ConexionesDisponiblesSincronizarConsultar().ToList();

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
        public Resultado Post(string IdConexion)
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };
            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {
                    objMINASBDEntities.ActualizarFechaConexion(Convert.ToInt32(IdConexion));
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
