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
    public class JobController : ApiController
    {


        public Resultado get()
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };

            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {

                    List<Models.ConsultarDisponibilidadJob_Result> LstJob = objMINASBDEntities.ConsultarDisponibilidadJob().ToList();
                    objResultado.Data = LstJob[0];

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
