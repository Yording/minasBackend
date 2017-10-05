﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppMinas.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class minasDBEntities : DbContext
    {
        public minasDBEntities()
            : base("name=minasDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Campo> Campo { get; set; }
        public virtual DbSet<Conexion> Conexion { get; set; }
        public virtual DbSet<Detalle> Detalle { get; set; }
        public virtual DbSet<Estructura> Estructura { get; set; }
        public virtual DbSet<Formulario> Formulario { get; set; }
        public virtual DbSet<Formulario1> Formulario1 { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Locacion> Locacion { get; set; }
        public virtual DbSet<TipoConexion> TipoConexion { get; set; }
        public virtual DbSet<TipoDato> TipoDato { get; set; }
        public virtual DbSet<TipoDetalle> TipoDetalle { get; set; }
        public virtual DbSet<TipoLocacion> TipoLocacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    
        public virtual int AddColumna(string nombreTabla, string nombreColumna, string tipoColumna, string obligatorio)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var nombreColumnaParameter = nombreColumna != null ?
                new ObjectParameter("NombreColumna", nombreColumna) :
                new ObjectParameter("NombreColumna", typeof(string));
    
            var tipoColumnaParameter = tipoColumna != null ?
                new ObjectParameter("TipoColumna", tipoColumna) :
                new ObjectParameter("TipoColumna", typeof(string));
    
            var obligatorioParameter = obligatorio != null ?
                new ObjectParameter("Obligatorio", obligatorio) :
                new ObjectParameter("Obligatorio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddColumna", nombreTablaParameter, nombreColumnaParameter, tipoColumnaParameter, obligatorioParameter);
        }
    
        public virtual int AddMedia(Nullable<int> idConexion, string urlDetalle, Nullable<int> idTipoDetalle, Nullable<int> idActividad, string descripcion, string nombreActividad)
        {
            var idConexionParameter = idConexion.HasValue ?
                new ObjectParameter("idConexion", idConexion) :
                new ObjectParameter("idConexion", typeof(int));
    
            var urlDetalleParameter = urlDetalle != null ?
                new ObjectParameter("UrlDetalle", urlDetalle) :
                new ObjectParameter("UrlDetalle", typeof(string));
    
            var idTipoDetalleParameter = idTipoDetalle.HasValue ?
                new ObjectParameter("idTipoDetalle", idTipoDetalle) :
                new ObjectParameter("idTipoDetalle", typeof(int));
    
            var idActividadParameter = idActividad.HasValue ?
                new ObjectParameter("idActividad", idActividad) :
                new ObjectParameter("idActividad", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("descripcion", descripcion) :
                new ObjectParameter("descripcion", typeof(string));
    
            var nombreActividadParameter = nombreActividad != null ?
                new ObjectParameter("NombreActividad", nombreActividad) :
                new ObjectParameter("NombreActividad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddMedia", idConexionParameter, urlDetalleParameter, idTipoDetalleParameter, idActividadParameter, descripcionParameter, nombreActividadParameter);
        }
    
        public virtual int AddRegistro(string nombreTabla, string values, string columns)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var valuesParameter = values != null ?
                new ObjectParameter("Values", values) :
                new ObjectParameter("Values", typeof(string));
    
            var columnsParameter = columns != null ?
                new ObjectParameter("Columns", columns) :
                new ObjectParameter("Columns", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddRegistro", nombreTablaParameter, valuesParameter, columnsParameter);
        }
    
        public virtual int AddRegla(string nombreTabla, string nombreColumna, string tablaReferencia, string nombreColumnaReferencia)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var nombreColumnaParameter = nombreColumna != null ?
                new ObjectParameter("NombreColumna", nombreColumna) :
                new ObjectParameter("NombreColumna", typeof(string));
    
            var tablaReferenciaParameter = tablaReferencia != null ?
                new ObjectParameter("TablaReferencia", tablaReferencia) :
                new ObjectParameter("TablaReferencia", typeof(string));
    
            var nombreColumnaReferenciaParameter = nombreColumnaReferencia != null ?
                new ObjectParameter("NombreColumnaReferencia", nombreColumnaReferencia) :
                new ObjectParameter("NombreColumnaReferencia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddRegla", nombreTablaParameter, nombreColumnaParameter, tablaReferenciaParameter, nombreColumnaReferenciaParameter);
        }
    
        public virtual int ChangeNameColumn(string tableName, string oldColumnName, string newColumnName)
        {
            var tableNameParameter = tableName != null ?
                new ObjectParameter("TableName", tableName) :
                new ObjectParameter("TableName", typeof(string));
    
            var oldColumnNameParameter = oldColumnName != null ?
                new ObjectParameter("OldColumnName", oldColumnName) :
                new ObjectParameter("OldColumnName", typeof(string));
    
            var newColumnNameParameter = newColumnName != null ?
                new ObjectParameter("NewColumnName", newColumnName) :
                new ObjectParameter("NewColumnName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ChangeNameColumn", tableNameParameter, oldColumnNameParameter, newColumnNameParameter);
        }
    
        public virtual ObjectResult<ColumnaClavePrimaria_Result> ColumnaClavePrimaria(string nombreTabla, string nombreColumna)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var nombreColumnaParameter = nombreColumna != null ?
                new ObjectParameter("NombreColumna", nombreColumna) :
                new ObjectParameter("NombreColumna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ColumnaClavePrimaria_Result>("ColumnaClavePrimaria", nombreTablaParameter, nombreColumnaParameter);
        }
    
        public virtual ObjectResult<ColumnaExiste_Result> ColumnaExiste(string nombreTabla, string nombreColumna)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var nombreColumnaParameter = nombreColumna != null ?
                new ObjectParameter("NombreColumna", nombreColumna) :
                new ObjectParameter("NombreColumna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ColumnaExiste_Result>("ColumnaExiste", nombreTablaParameter, nombreColumnaParameter);
        }
    
        public virtual int ConsultarFormularios(string idActividad, string formulario, string fechaInicio, string fechaFin)
        {
            var idActividadParameter = idActividad != null ?
                new ObjectParameter("IdActividad", idActividad) :
                new ObjectParameter("IdActividad", typeof(string));
    
            var formularioParameter = formulario != null ?
                new ObjectParameter("Formulario", formulario) :
                new ObjectParameter("Formulario", typeof(string));
    
            var fechaInicioParameter = fechaInicio != null ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(string));
    
            var fechaFinParameter = fechaFin != null ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ConsultarFormularios", idActividadParameter, formularioParameter, fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual int CrearTabla(string nombreTabla)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CrearTabla", nombreTablaParameter);
        }
    
        public virtual int Prueba(string nombreTabla, string nombreColumna, string tipoColumna, string obligatorio)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var nombreColumnaParameter = nombreColumna != null ?
                new ObjectParameter("NombreColumna", nombreColumna) :
                new ObjectParameter("NombreColumna", typeof(string));
    
            var tipoColumnaParameter = tipoColumna != null ?
                new ObjectParameter("TipoColumna", tipoColumna) :
                new ObjectParameter("TipoColumna", typeof(string));
    
            var obligatorioParameter = obligatorio != null ?
                new ObjectParameter("Obligatorio", obligatorio) :
                new ObjectParameter("Obligatorio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Prueba", nombreTablaParameter, nombreColumnaParameter, tipoColumnaParameter, obligatorioParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> RegistroExiste(string nombreTabla, string nombreColumna, string idPK)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var nombreColumnaParameter = nombreColumna != null ?
                new ObjectParameter("NombreColumna", nombreColumna) :
                new ObjectParameter("NombreColumna", typeof(string));
    
            var idPKParameter = idPK != null ?
                new ObjectParameter("IdPK", idPK) :
                new ObjectParameter("IdPK", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("RegistroExiste", nombreTablaParameter, nombreColumnaParameter, idPKParameter);
        }
    
        public virtual ObjectResult<TablaExiste_Result> TablaExiste(string nombreTabla)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TablaExiste_Result>("TablaExiste", nombreTablaParameter);
        }
    
        public virtual ObjectResult<string> TablasConsulta(string nombreTabla)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("TablasConsulta", nombreTablaParameter);
        }
    
        public virtual int UpdateRegistro(string nombreTabla, string script, string idActividad)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var scriptParameter = script != null ?
                new ObjectParameter("script", script) :
                new ObjectParameter("script", typeof(string));
    
            var idActividadParameter = idActividad != null ?
                new ObjectParameter("IdActividad", idActividad) :
                new ObjectParameter("IdActividad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateRegistro", nombreTablaParameter, scriptParameter, idActividadParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ValidarRegistroActualizar(string nombreTabla, string updateOn, string idActividad)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var updateOnParameter = updateOn != null ?
                new ObjectParameter("UpdateOn", updateOn) :
                new ObjectParameter("UpdateOn", typeof(string));
    
            var idActividadParameter = idActividad != null ?
                new ObjectParameter("IdActividad", idActividad) :
                new ObjectParameter("IdActividad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ValidarRegistroActualizar", nombreTablaParameter, updateOnParameter, idActividadParameter);
        }
    
        public virtual int EliminarDetalleActividad(string nombreTabla, string idActividad)
        {
            var nombreTablaParameter = nombreTabla != null ?
                new ObjectParameter("NombreTabla", nombreTabla) :
                new ObjectParameter("NombreTabla", typeof(string));
    
            var idActividadParameter = idActividad != null ?
                new ObjectParameter("IdActividad", idActividad) :
                new ObjectParameter("IdActividad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarDetalleActividad", nombreTablaParameter, idActividadParameter);
        }
    
        public virtual ObjectResult<ConsultarDisponibilidadJob_Result> ConsultarDisponibilidadJob()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConsultarDisponibilidadJob_Result>("ConsultarDisponibilidadJob");
        }
    
        public virtual int ActualizarFechaConexion(Nullable<int> idConexion)
        {
            var idConexionParameter = idConexion.HasValue ?
                new ObjectParameter("IdConexion", idConexion) :
                new ObjectParameter("IdConexion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarFechaConexion", idConexionParameter);
        }
    
        public virtual ObjectResult<ConexionesDisponiblesSincronizarConsultar_Result> ConexionesDisponiblesSincronizarConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConexionesDisponiblesSincronizarConsultar_Result>("ConexionesDisponiblesSincronizarConsultar");
        }
    
        public virtual int ConsultarActividades(string formulario, string fechaInicio, string fechaFin)
        {
            var formularioParameter = formulario != null ?
                new ObjectParameter("Formulario", formulario) :
                new ObjectParameter("Formulario", typeof(string));
    
            var fechaInicioParameter = fechaInicio != null ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(string));
    
            var fechaFinParameter = fechaFin != null ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ConsultarActividades", formularioParameter, fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
