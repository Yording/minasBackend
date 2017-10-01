using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using AppMinas.Models;

namespace AppMinas.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using AppMinas.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Conexion>("Conexiones");
    builder.EntitySet<Job>("Job"); 
    builder.EntitySet<Estructura>("Estructura"); 
    builder.EntitySet<Formulario>("Formulario"); 
    builder.EntitySet<Detalle>("Detalle"); 
    builder.EntitySet<TipoConexion>("TipoConexion"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ConexionesController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/Conexiones
        [EnableQuery]
        public IQueryable<Conexion> GetConexiones()
        {
            return db.Conexion;
        }

        // GET: odata/Conexiones(5)
        [EnableQuery]
        public SingleResult<Conexion> GetConexion([FromODataUri] int key)
        {
            return SingleResult.Create(db.Conexion.Where(conexion => conexion.idConexion == key));
        }

        // PUT: odata/Conexiones(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Conexion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Conexion conexion = await db.Conexion.FindAsync(key);
            if (conexion == null)
            {
                return NotFound();
            }

            patch.Put(conexion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConexionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(conexion);
        }

        // POST: odata/Conexiones
        public async Task<IHttpActionResult> Post(Conexion conexion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Conexion.Add(conexion);
            await db.SaveChangesAsync();

            return Created(conexion);
        }

        // PATCH: odata/Conexiones(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Conexion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Conexion conexion = await db.Conexion.FindAsync(key);
            if (conexion == null)
            {
                return NotFound();
            }

            patch.Patch(conexion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConexionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(conexion);
        }

        // DELETE: odata/Conexiones(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Conexion conexion = await db.Conexion.FindAsync(key);
            if (conexion == null)
            {
                return NotFound();
            }

            db.Conexion.Remove(conexion);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Conexiones(5)/Job
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] int key)
        {
            return SingleResult.Create(db.Conexion.Where(m => m.idConexion == key).Select(m => m.Job));
        }

        // GET: odata/Conexiones(5)/Estructura
        [EnableQuery]
        public IQueryable<Estructura> GetEstructura([FromODataUri] int key)
        {
            return db.Conexion.Where(m => m.idConexion == key).SelectMany(m => m.Estructura);
        }

        // GET: odata/Conexiones(5)/Formulario
        [EnableQuery]
        public SingleResult<Formulario> GetFormulario([FromODataUri] int key)
        {
            return SingleResult.Create(db.Conexion.Where(m => m.idConexion == key).Select(m => m.Formulario));
        }

        // GET: odata/Conexiones(5)/Detalle
        [EnableQuery]
        public IQueryable<Detalle> GetDetalle([FromODataUri] int key)
        {
            return db.Conexion.Where(m => m.idConexion == key).SelectMany(m => m.Detalle);
        }

        // GET: odata/Conexiones(5)/TipoConexion
        [EnableQuery]
        public SingleResult<TipoConexion> GetTipoConexion([FromODataUri] int key)
        {
            return SingleResult.Create(db.Conexion.Where(m => m.idConexion == key).Select(m => m.TipoConexion));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConexionExists(int key)
        {
            return db.Conexion.Count(e => e.idConexion == key) > 0;
        }
    }
}
