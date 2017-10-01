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
    builder.EntitySet<TipoLocacion>("TipoLocaciones");
    builder.EntitySet<Locacion>("Locacion"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TipoLocacionesController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/TipoLocaciones
        [EnableQuery]
        public IQueryable<TipoLocacion> GetTipoLocaciones()
        {
            return db.TipoLocacion;
        }

        // GET: odata/TipoLocaciones(5)
        [EnableQuery]
        public SingleResult<TipoLocacion> GetTipoLocacion([FromODataUri] int key)
        {
            return SingleResult.Create(db.TipoLocacion.Where(tipoLocacion => tipoLocacion.idTipoLocacion == key));
        }

        // PUT: odata/TipoLocaciones(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TipoLocacion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoLocacion tipoLocacion = await db.TipoLocacion.FindAsync(key);
            if (tipoLocacion == null)
            {
                return NotFound();
            }

            patch.Put(tipoLocacion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoLocacionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoLocacion);
        }

        // POST: odata/TipoLocaciones
        public async Task<IHttpActionResult> Post(TipoLocacion tipoLocacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoLocacion.Add(tipoLocacion);
            await db.SaveChangesAsync();

            return Created(tipoLocacion);
        }

        // PATCH: odata/TipoLocaciones(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TipoLocacion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoLocacion tipoLocacion = await db.TipoLocacion.FindAsync(key);
            if (tipoLocacion == null)
            {
                return NotFound();
            }

            patch.Patch(tipoLocacion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoLocacionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tipoLocacion);
        }

        // DELETE: odata/TipoLocaciones(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TipoLocacion tipoLocacion = await db.TipoLocacion.FindAsync(key);
            if (tipoLocacion == null)
            {
                return NotFound();
            }

            db.TipoLocacion.Remove(tipoLocacion);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TipoLocaciones(5)/Locacion
        [EnableQuery]
        public IQueryable<Locacion> GetLocacion([FromODataUri] int key)
        {
            return db.TipoLocacion.Where(m => m.idTipoLocacion == key).SelectMany(m => m.Locacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoLocacionExists(int key)
        {
            return db.TipoLocacion.Count(e => e.idTipoLocacion == key) > 0;
        }
    }
}
