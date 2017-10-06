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
    builder.EntitySet<Formulario>("Formularios");
    builder.EntitySet<Conexion>("Conexion"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FormulariosController : ODataController
    {
        private minasDBEntities db = new minasDBEntities();

        // GET: odata/Formularios
        [EnableQuery]
        public IQueryable<Formulario> GetFormularios()
        {
            return db.Formulario;
        }

        // GET: odata/Formularios(5)
        [EnableQuery]
        public SingleResult<Formulario> GetFormulario([FromODataUri] int key)
        {
            return SingleResult.Create(db.Formulario.Where(formulario => formulario.idFormulario == key));
        }

        // PUT: odata/Formularios(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Formulario> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Formulario formulario = await db.Formulario.FindAsync(key);
            if (formulario == null)
            {
                return NotFound();
            }

            patch.Put(formulario);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormularioExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(formulario);
        }

        // POST: odata/Formularios
        public async Task<IHttpActionResult> Post(Formulario formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Formulario.Add(formulario);
            await db.SaveChangesAsync();

            return Created(formulario);
        }

        // PATCH: odata/Formularios(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Formulario> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Formulario formulario = await db.Formulario.FindAsync(key);
            if (formulario == null)
            {
                return NotFound();
            }

            patch.Patch(formulario);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormularioExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(formulario);
        }

        // DELETE: odata/Formularios(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Formulario formulario = await db.Formulario.FindAsync(key);
            if (formulario == null)
            {
                return NotFound();
            }

            db.Formulario.Remove(formulario);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Formularios(5)/Conexion
        [EnableQuery]
        public IQueryable<Conexion> GetConexion([FromODataUri] int key)
        {
            return db.Formulario.Where(m => m.idFormulario == key).SelectMany(m => m.Conexion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FormularioExists(int key)
        {
            return db.Formulario.Count(e => e.idFormulario == key) > 0;
        }
    }
}
