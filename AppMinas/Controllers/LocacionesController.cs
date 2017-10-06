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
    builder.EntitySet<Locacion>("Locaciones");
    builder.EntitySet<Formulario1>("Formulario1"); 
    builder.EntitySet<TipoLocacion>("TipoLocacion"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LocacionesController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/Locaciones
        [EnableQuery]
        public IQueryable<Locacion> GetLocaciones()
        {
            return db.Locacion;
        }

        // GET: odata/Locaciones(5)
        [EnableQuery]
        public SingleResult<Locacion> GetLocacion([FromODataUri] long key)
        {
            return SingleResult.Create(db.Locacion.Where(locacion => locacion.idLocacion == key));
        }

        // PUT: odata/Locaciones(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<Locacion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Locacion locacion = await db.Locacion.FindAsync(key);
            if (locacion == null)
            {
                return NotFound();
            }

            patch.Put(locacion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(locacion);
        }

        // POST: odata/Locaciones
        public async Task<IHttpActionResult> Post(Locacion locacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locacion.Add(locacion);
            await db.SaveChangesAsync();

            return Created(locacion);
        }

        // PATCH: odata/Locaciones(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<Locacion> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Locacion locacion = await db.Locacion.FindAsync(key);
            if (locacion == null)
            {
                return NotFound();
            }

            patch.Patch(locacion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(locacion);
        }

        // DELETE: odata/Locaciones(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            Locacion locacion = await db.Locacion.FindAsync(key);
            if (locacion == null)
            {
                return NotFound();
            }

            db.Locacion.Remove(locacion);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: odata/Locaciones(5)/TipoLocacion
        [EnableQuery]
        public SingleResult<TipoLocacion> GetTipoLocacion([FromODataUri] long key)
        {
            return SingleResult.Create(db.Locacion.Where(m => m.idLocacion == key).Select(m => m.TipoLocacion));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocacionExists(long key)
        {
            return db.Locacion.Count(e => e.idLocacion == key) > 0;
        }
    }
}
