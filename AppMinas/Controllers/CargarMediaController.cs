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

namespace AppMinas.Controllers
{
    public class CargarMediaController : ApiController
    {

        private struct _Constantes
        {
           
            public const string Media = "files[]";
            public const string idConexion = "idConexion";
            //public const string UrlDetalle = "UrlDetalle";
            public const string idTipoDetalle = "idTipoDetalle";
            public const string idActividad = "idActividad";
            public const string Descripcion = "Descripcion";
            public const string NombreActividad = "NombreActividad";

        }

        public Resultado Post()
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };

            NameValueCollection FormValue = System.Web.HttpContext.Current.Request.Form;

            string Media = FormValue[_Constantes.Media];
            int idConexion = Convert.ToInt32(FormValue[_Constantes.idConexion]);
            //string UrlDetalle = FormValue[_Constantes.UrlDetalle];
            int idTipoDetalle = Convert.ToInt32(FormValue[_Constantes.idTipoDetalle]);
            string idActividad = FormValue[_Constantes.idActividad];
            string Descripcion = FormValue[_Constantes.Descripcion];
            string NombreActividad = FormValue[_Constantes.NombreActividad];

            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {
                    HttpFileCollection Files = HttpContext.Current.Request.Files;
                    var i = 0;
                    foreach (var file in Files)
                    {
                        Guid g;
                        g = Guid.NewGuid();
                        string NombreArchivo = "M" + g.ToString();
                        if (file.ToString() == string.Format("files[{0}]", i))
                        {


                            string NombreArchivoReal = Files[string.Format("files[{0}]",i)].FileName;
                            string[] ArrayName = NombreArchivoReal.Split('.');


                            Media = UtilidadesAzure.GuardarArchivo("mycontainer", ArrayName[0] + "-" + NombreArchivo, Files[string.Format("files[{0}]", i)]); //La urlDetalle
                            objMINASBDEntities.AddMedia(idConexion, Media, idTipoDetalle, idActividad, Descripcion, NombreActividad);

                            objResultado.Mensaje = "Se inserto el contenido multimedia correctamente";
                            objResultado.TipoResultado = true;
                            i++;
                        }
                        else

                            objResultado.Mensaje = "No se ha trasmitido el archivo al servidor";
                        objResultado.TipoResultado = false;
                    }
                }
                  
                        

            }
            catch (Exception ex)
            {
          
                objResultado.Mensaje = ex.Message;
                objResultado.TipoResultado = false;
               
            }

            return objResultado;
        }




    }
}
