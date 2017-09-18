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
    builder.EntitySet<TipoDetalle>("TipoDetalles");
    builder.EntitySet<Detalle>("Detalle"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TipoDetallesController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/TipoDetalles
        [EnableQuery]
        public IQueryable<TipoDetalle> GetTipoDetalles()
        {
            return db.TipoDetalle;
        }

        // GET: odata/TipoDetalles(5)
        [EnableQuery]
        public SingleResult<TipoDetalle> GetTipoDetalle([FromODataUri] int key)
        {
            return SingleResult.Create(db.TipoDetalle.Where(tipoDetalle => tipoDetalle.idTipoDetalle == key));
        }

        // PUT: odata/TipoDetalles(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TipoDetalle> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDetalle tipoDetalle = await db.TipoDetalle.FindAsync(key);
            if (tipoDetalle == null)
            {
                return NotFound();
            }

            patch.Put(tipoDetalle);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDetalleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoDetalle);
        }

        // POST: odata/TipoDetalles
        public async Task<IHttpActionResult> Post(TipoDetalle tipoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoDetalle.Add(tipoDetalle);
            await db.SaveChangesAsync();

            return Created(tipoDetalle);
        }

        // PATCH: odata/TipoDetalles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TipoDetalle> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDetalle tipoDetalle = await db.TipoDetalle.FindAsync(key);
            if (tipoDetalle == null)
            {
                return NotFound();
            }

            patch.Patch(tipoDetalle);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDetalleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoDetalle);
        }

        // DELETE: odata/TipoDetalles(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TipoDetalle tipoDetalle = await db.TipoDetalle.FindAsync(key);
            if (tipoDetalle == null)
            {
                return NotFound();
            }

            db.TipoDetalle.Remove(tipoDetalle);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TipoDetalles(5)/Detalle
        [EnableQuery]
        public IQueryable<Detalle> GetDetalle([FromODataUri] int key)
        {
            return db.TipoDetalle.Where(m => m.idTipoDetalle == key).SelectMany(m => m.Detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoDetalleExists(int key)
        {
            return db.TipoDetalle.Count(e => e.idTipoDetalle == key) > 0;
        }
    }
}
