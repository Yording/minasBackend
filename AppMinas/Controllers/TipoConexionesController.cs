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
    builder.EntitySet<TipoConexion>("TipoConexiones");
    builder.EntitySet<Conexion>("Conexion"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TipoConexionesController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/TipoConexiones
        [EnableQuery]
        public IQueryable<TipoConexion> GetTipoConexiones()
        {
            return db.TipoConexion;
        }

        // GET: odata/TipoConexiones(5)
        [EnableQuery]
        public SingleResult<TipoConexion> GetTipoConexion([FromODataUri] int key)
        {
            return SingleResult.Create(db.TipoConexion.Where(tipoConexion => tipoConexion.idTipoConexion == key));
        }

        // PUT: odata/TipoConexiones(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TipoConexion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoConexion tipoConexion = await db.TipoConexion.FindAsync(key);
            if (tipoConexion == null)
            {
                return NotFound();
            }

            patch.Put(tipoConexion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoConexionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoConexion);
        }

        // POST: odata/TipoConexiones
        public async Task<IHttpActionResult> Post(TipoConexion tipoConexion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoConexion.Add(tipoConexion);
            await db.SaveChangesAsync();

            return Created(tipoConexion);
        }

        // PATCH: odata/TipoConexiones(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TipoConexion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoConexion tipoConexion = await db.TipoConexion.FindAsync(key);
            if (tipoConexion == null)
            {
                return NotFound();
            }

            patch.Patch(tipoConexion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoConexionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoConexion);
        }

        // DELETE: odata/TipoConexiones(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TipoConexion tipoConexion = await db.TipoConexion.FindAsync(key);
            if (tipoConexion == null)
            {
                return NotFound();
            }

            db.TipoConexion.Remove(tipoConexion);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TipoConexiones(5)/Conexion
        [EnableQuery]
        public IQueryable<Conexion> GetConexion([FromODataUri] int key)
        {
            return db.TipoConexion.Where(m => m.idTipoConexion == key).SelectMany(m => m.Conexion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoConexionExists(int key)
        {
            return db.TipoConexion.Count(e => e.idTipoConexion == key) > 0;
        }
    }
}
