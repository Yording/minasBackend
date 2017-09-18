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
    builder.EntitySet<Detalle>("Detalles");
    builder.EntitySet<Conexion>("Conexion"); 
    builder.EntitySet<TipoDetalle>("TipoDetalle"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DetallesController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/Detalles
        [EnableQuery]
        public IQueryable<Detalle> GetDetalles()
        {
            return db.Detalle;
        }

        // GET: odata/Detalles(5)
        [EnableQuery]
        public SingleResult<Detalle> GetDetalle([FromODataUri] long key)
        {
            return SingleResult.Create(db.Detalle.Where(detalle => detalle.idDetalle == key));
        }

        // PUT: odata/Detalles(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<Detalle> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Detalle detalle = await db.Detalle.FindAsync(key);
            if (detalle == null)
            {
                return NotFound();
            }

            patch.Put(detalle);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(detalle);
        }

        // POST: odata/Detalles
        public async Task<IHttpActionResult> Post(Detalle detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Detalle.Add(detalle);
            await db.SaveChangesAsync();

            return Created(detalle);
        }

        // PATCH: odata/Detalles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<Detalle> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Detalle detalle = await db.Detalle.FindAsync(key);
            if (detalle == null)
            {
                return NotFound();
            }

            patch.Patch(detalle);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(detalle);
        }

        // DELETE: odata/Detalles(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            Detalle detalle = await db.Detalle.FindAsync(key);
            if (detalle == null)
            {
                return NotFound();
            }

            db.Detalle.Remove(detalle);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Detalles(5)/Conexion
        [EnableQuery]
        public SingleResult<Conexion> GetConexion([FromODataUri] long key)
        {
            return SingleResult.Create(db.Detalle.Where(m => m.idDetalle == key).Select(m => m.Conexion));
        }

        // GET: odata/Detalles(5)/TipoDetalle
        [EnableQuery]
        public SingleResult<TipoDetalle> GetTipoDetalle([FromODataUri] long key)
        {
            return SingleResult.Create(db.Detalle.Where(m => m.idDetalle == key).Select(m => m.TipoDetalle));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetalleExists(long key)
        {
            return db.Detalle.Count(e => e.idDetalle == key) > 0;
        }
    }
}
