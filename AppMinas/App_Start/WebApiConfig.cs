using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using AppMinas.Models;

namespace AppMinas
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            // Web API configuration and services

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Job>("Jobs");
            builder.EntitySet<Conexion>("Conexiones");
            builder.EntitySet<Detalle>("Detalles");
            builder.EntitySet<Formulario>("Formularios");
            builder.EntitySet<Locacion>("Locaciones");
            builder.EntitySet<TipoDetalle>("TipoDetalles");
            builder.EntitySet<TipoLocacion>("TipoLocaciones");
            builder.EntitySet<Usuario>("Usuarios");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}
