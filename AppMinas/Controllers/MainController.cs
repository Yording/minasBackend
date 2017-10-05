using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppMinas.Services;
using AppMinas.Listas;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AppMinas.Models;

namespace AppMinas.Controllers
{
    public class MainController : ApiController
    {
        //Atributos
        private static HelperService helper;
        private static AuthService authService;
        private static FormService formService;
        private static UserService userService;
        private static LocationService locationService;
        private static ActivityService activityService;
        private static ConnectionService connectionService;
        private static string _token = string.Empty;

        public void insertarFormularios(List<Form> formsVicitrack)
        {
            //Invertimos la lista para traer los ultimos formularios
            formsVicitrack.Reverse();
            // Insertar formularios
            foreach (Form ele in formsVicitrack)
            {
                // Verificamos si el formulario ya existe en la BD
                JObject response = JObject.Parse(formService.findFormGUID(ele.GUID));
                if (response.Value<Newtonsoft.Json.Linq.JToken>("value").Count<JToken>() == 0)
                {
                    //tenemos algunos formularios repetidos
                    formService.createForm(ele.GUID, ele.Title);
                }
                else
                {
                    break;
                }
            }

        }

        public void insertarUsuarios(List<User> usersVicitrack)
        {
            //Invertimos la lista para traer los ultimos usuarios
            usersVicitrack.Reverse();
            // Insertar formularios
            foreach (User ele in usersVicitrack)
            {
                // Verificamos si el usuario ya existe en la BD
                JObject response = JObject.Parse(userService.findUserId(ele.ID));
                if (response.Value<Newtonsoft.Json.Linq.JToken>("value").Count<JToken>() == 0)
                {
                    //tenemos algunos formularios repetidos
                    userService.createUser(ele.ID, ele.FirstName, ele.LastName);
                }
                else
                {
                    break;
                }
            }

        }

        public void insertarLocaciones(List<Location> locationsVicitrack)
        {
            //Invertimos la lista para traer los ultimos formularios
            locationsVicitrack.Reverse();
            // Insertar formularios
            foreach (Location ele in locationsVicitrack)
            {
                // Verificamos si el formulario ya existe en la BD
                JObject response = JObject.Parse(formService.findFormGUID(ele.GUID));
                if (response.Value<Newtonsoft.Json.Linq.JToken>("value").Count<JToken>() == 0)
                {
                    //tenemos algunos formularios repetidos
                    formService.createForm(ele.GUID, ele.Fax);
                }
                else
                {
                    break;
                }
            }

        }

        public void insertarTipoLocaciones(List<TypeLocation> typeLocationsVicitrack)
        {
            //Invertimos la lista para traer los ultimos formularios
            typeLocationsVicitrack.Reverse();
            // Insertar formularios
            foreach (TypeLocation ele in typeLocationsVicitrack)
            {
                // Verificamos si el formulario ya existe en la BD
                JObject response = JObject.Parse(locationService.findTypeLocationGUID(ele.GUID));
                if (response.Value<Newtonsoft.Json.Linq.JToken>("value").Count<JToken>() == 0)
                {
                    //tenemos algunos formularios repetidos
                    locationService.createTypeLocation(ele.GUID, ele.Name, ele.IsActive);
                }
                else
                {
                    break;
                }
            }

        }

        /* public async void Run()
         {
             Token authentication = JsonConvert.DeserializeObject<Token>(authService.getAuthentication());
             if (authentication.status == "OK")
             {
                 // Se obtiene el token de acceso que devolvio la api
                 string _token = authentication.AccessToken;

                 // Instancias a utlizar
                 activityService = new ActivityService(_token);
                 formService = new FormService(_token);
                 connectionService = new ConnectionService();

                 // Se Obtiene una lista con todos los formularios alojados en la api
                 List<Form> formsVicitrack = JsonConvert.DeserializeObject<List<Form>>(formService.getForms());

                 // Obtener las conexiones creadas
                 ResponseModel responseConnections = JsonConvert.DeserializeObject<ResponseModel>(connectionService.getConnectionsForms());
                 List<ConexionModel> connectionsForms = JsonConvert.DeserializeObject<List<ConexionModel>>(responseConnections.value.ToString());


                 // Se obtiene una lista con las conexiones creadas en la BD.


                 insertarFormularios(formsVicitrack);


                 // Se obtiene la lista con todas las actividades de la api
                 List<Activity> activiesVicitrack = JsonConvert.DeserializeObject<List<Activity>>(activityService.getActivity());

                 // Foreach de todos las actividades(registros en los formularios)
                 activiesVicitrack.ForEach(ele => {
                     DetailActivity activiesDetailVicitrack = JsonConvert.DeserializeObject<DetailActivity>(activityService.getDetailActivity(ele.GUID));
                     ResponseModel responseFindForm = JsonConvert.DeserializeObject<ResponseModel>(connectionService.findConnectionGUIDForms(activiesDetailVicitrack.FormGUID));
                     if (responseFindForm.value.ToString() != "[]")
                     {
                         activiesDetailVicitrack.Values.ForEach(value =>
                         {
                           string Datos =string.Format("Del formulario {0} Campo {1} y Tipo de dato {2}", activiesDetailVicitrack.Title, value.apiId, value.Value.GetType());
                         });
                     }
                 });



                 // Prueba para convertir un JArray a una lista tipada
                 //JObject activiesDetailVicitrack = JObject.Parse(activityService.getDetailActivity("e36e60d0-0fb8-4cb9-980a-e628f9524537"));
                 //var jarr = activiesDetailVicitrack["Values"].Value<JArray>();
                 //List<FormValues> lst = jarr.ToObject<List<FormValues>>();
                 //var dasd = jarr[2]["Value"].Value<JArray>();
                 //List<FormValues> lst2 = dasd.ToObject<List<FormValues>>();
                 //var dasdsad = lst2[0].Value;

                 // Prueba con Object para saber que puede ser varios tipos de datos
                 // con GetType puedo saber el tipo de dato
                 //object pad = 1;
                 //object das = "dasd";
                 //object ara = new Array [1, 23, 3];
                 //Console.WriteLine("{0},{1},{2}",pad.GetType(), das.GetType(), ara.GetType());
             }
         }*/


        public bool main(int idJob)
        {
            //Cambio Mateo 2

            using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
            {
                Token authentication = JsonConvert.DeserializeObject<Token>(authService.getAuthentication());
                if (authentication.status == "OK")
                {

                    // Se obtiene el token de acceso que devolvio la api
                    string _token = authentication.AccessToken;

                    // Instancias a utlizar
                    activityService = new ActivityService(_token);
                    formService = new FormService(_token);
                    userService = new UserService(_token);
                    locationService = new LocationService(_token);
                    connectionService = new ConnectionService();

                    // Se Obtiene una lista con todos los formularios alojados en la api
                    List<Form> formsVicitrack = JsonConvert.DeserializeObject<List<Form>>(formService.getForms());

                    // Se Obtiene una lista con todos los usuarios alojados en la api
                    List<User> usersVicitrack = JsonConvert.DeserializeObject<List<User>>(userService.getUsers());

                    // Se Obtiene una lista con todos las tipo locaciones alojados en la api
                    List<TypeLocation> typeLocationsVicitrack = JsonConvert.DeserializeObject<List<TypeLocation>>(locationService.getTypeLocations());

                    // Obtener las conexiones creadas
                    ResponseModel responseConnections = JsonConvert.DeserializeObject<ResponseModel>(connectionService.getConnectionsForms());

                    // Se obtiene una lista con las conexiones creadas en la BD.
                    //List<ConexionModel> connectionsForms = JsonConvert.DeserializeObject<List<ConexionModel>>(responseConnections.value.ToString());
                    List<ConexionesDisponiblesSincronizarConsultar_Result> connectionsSincronizar = ConexionesDisponiblesSincronizar(idJob);

                    // Insertamos los formularios de vicitrack a la tabla Formularios
                    insertarFormularios(formsVicitrack);

                    // Insertamos los usuarios de vicitrack a la tabla Usuarios
                    insertarUsuarios(usersVicitrack);

                    // Insertamos los tipos de locaciones de vicitrack a la tabla TipoLocacion
                    insertarTipoLocaciones(typeLocationsVicitrack);


                    // Se obtiene la lista con todas las actividades de la api
                    List<Activity> activiesVicitrack = JsonConvert.DeserializeObject<List<Activity>>(activityService.getActivity());

                    int i = 0; // Actividades
                    int j = 0; //Campos de Actividades
                    string FormGuid = "";
                    bool BandIDColumn = false;
                    ArrayList columnasStringsNames = new ArrayList();
                    ArrayList columnasDetalleNames = new ArrayList();

                    //ArrayList Datos = new ArrayList(); // en este array almaceno los datos individuales

                    //TODO----Se agregan diccionarios para controlar varias tablas
                    // Este diccionario la clave será la nombre de la tabla y valor los registros
                    Dictionary<string, ArrayList> DictTablasColumnas = new Dictionary<string, ArrayList>();
                    Dictionary<string, ArrayList> DictTablasColumnasActualizar = new Dictionary<string, ArrayList>();
                    //Este diccionario sirve para guardar las columnas de cada una de los detalles para cada formulario
                    Dictionary<string, Dictionary<string, ArrayList>> DictTablasColumnasDetalles = new Dictionary<string, Dictionary<string, ArrayList>>();
                    //Este diccionario sirve para obtener todas las columnas detalle de un formulario
                    Dictionary<string, ArrayList> DictTablasColumnasFormulariosDetalles = new Dictionary<string, ArrayList>();
                    Dictionary<string, ArrayList> DictTablasDatos= new Dictionary<string, ArrayList>();
                    Dictionary<string, Dictionary<string, ArrayList>> DictTablasDatosDetalles = new Dictionary<string, Dictionary<string, ArrayList>>();
                    Dictionary<string, Dictionary<string, ArrayList>> DictTablasDatosDetallesActualizar = new Dictionary<string, Dictionary<string, ArrayList>>();
                    Dictionary<string, ArrayList> DictTablasDatosActualizar = new Dictionary<string, ArrayList>();

                    // TODO----No se requiere estas variables seran reemplazadas por diccionarios
                    ArrayList DatosColumnas = new ArrayList(); // en este array meto todos los Formularios que se encuentran en Datos 
                    ArrayList DatosColumnasActualizar = new ArrayList(); // en este array meto todos los Formularios que se encuentran en Datos donde se encuentre la validacion para actualizar
                    ArrayList DatosDetalles = new ArrayList();

                    string TableName = "";
                    string ColumnaActividadExiste = "ID";
                    bool Existe = false;
                    bool Actualizar = false;

                    //Validar si existen conexiones
                    if (connectionsSincronizar.Count > 0)
                    {
                        // Foreach de todos las actividades(registros en los formularios)
                        foreach (Activity ele in activiesVicitrack)
                        {
                            DetailActivity activiesDetailVicitrack = JsonConvert.DeserializeObject<DetailActivity>(activityService.getDetailActivity(ele.GUID));
                            // TODO--------Se elimina esta variable responseFindForm
                            //ResponseModel responseFindForm = JsonConvert.DeserializeObject<ResponseModel>(connectionService.findConnectionGUIDForms(activiesDetailVicitrack.FormGUID));
                            var res = ExisteConexion(connectionsSincronizar, activiesDetailVicitrack.FormGUID);

                            if (res)
                            {
                                j = 0;
                                ArrayList Datos = new ArrayList();
                                ArrayList DatosActualizar = new ArrayList();
                                foreach (var value in activiesDetailVicitrack.Values)
                                {
                                   
                                    // Si es j==0 va agregar los datos básicos de la actividad a un nuevo registro
                                   if (j == 0)
                                    {
                                        //Solo vamos a entrar en este fragmento de codigo en el primer recorrido de datos de cada actividad
                                        FormGuid = activiesDetailVicitrack.FormGUID;
                                        TableName = "F" + FormGuid;
                                       
                                        // Crea la key con el nombre de la tabla en caso de que la llave aun no exista
                                        
                                        if (!DictTablasColumnas.ContainsKey(TableName))
                                        {
                                            DictTablasColumnas.Add(TableName, new ArrayList { });
                                            //DictTablasColumnasActualizar.Add(TableName, new ArrayList { });
                                            DictTablasDatos.Add(TableName, new ArrayList { });
                                            DictTablasDatosActualizar.Add(TableName, new ArrayList { });
                                            DictTablasColumnasFormulariosDetalles.Add(TableName, new ArrayList { });
                                            DictTablasColumnasDetalles.Add(TableName, new Dictionary<string, ArrayList> { });
                                            DictTablasDatosDetalles.Add(TableName, new Dictionary<string, ArrayList> { });
                                            DictTablasDatosDetallesActualizar.Add(TableName, new Dictionary<string, ArrayList> { });
                                            BandIDColumn = false;
                                            i = 0;
                                        }
                                       
                                        if (!RegistroExiste(TableName, ColumnaActividadExiste, activiesDetailVicitrack.ID.ToString()))
                                        {
                                            Existe = false;
                                        }
                                        else
                                        {
                                            Existe = true;
                                            //Existe el registro inv

                                            if (ValidarRegistroActualizar(TableName, activiesDetailVicitrack.UpdatedOn.ToString(), activiesDetailVicitrack.ID.ToString()))
                                            {
                                               Actualizar = true;

                                            }
                                            else
                                            {
                                                Actualizar = false;
                                            }

                                            //Cierre inv

                                           /* if (i != 0)
                                            {
                                             
                                                break;
                                            }*/

                                            
                                        }

                                     

                                        //Si es valido para actualizar, modifica el dato
                                        if (Actualizar) {
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.ID.ToString()));
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.Title.ToString()));
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.LocationName.ToString()));
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.LocationGUID.ToString()));
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.UpdatedOn.ToString()));
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.UpdatedOn.ToString()));
                                            DatosActualizar.Add(ConfigurarDato(activiesDetailVicitrack.UserName.ToString()));
                                        }
                                        else {

                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.ID.ToString()));
                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.Title.ToString()));
                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.LocationName.ToString()));
                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.LocationGUID.ToString()));
                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.CreatedOn.ToString()));
                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.UpdatedOn.ToString()));
                                            Datos.Add(ConfigurarDato(activiesDetailVicitrack.UserName.ToString()));
                                        }
                                        
                                      
                                    }

                                    //Obtener la estructura de los formulario solo en la primera actividad
                                    //TODO------Debo controlar solo ingrese un sola vez por formulario, con DictTablasDatos es suficiente con el numero registros igual a 0
                                    //Todo------Se puede eliminar variable i
                                    if (DictTablasDatos[TableName].Count == 0 && DictTablasDatosActualizar[TableName].Count==0)
                                    {
                                        if (!BandIDColumn)
                                        {
                                            DictTablasColumnas[TableName].Add("ID");
                                            DictTablasColumnas[TableName].Add("Title");
                                            DictTablasColumnas[TableName].Add("LocationName");
                                            DictTablasColumnas[TableName].Add("LocationGUID");
                                            DictTablasColumnas[TableName].Add("CreatedOn");
                                            DictTablasColumnas[TableName].Add("UpdatedOn");
                                            DictTablasColumnas[TableName].Add("UserName");
                                            BandIDColumn = true;

                                 
                                        }
                                        if (i == 0)
                                        {
                                            if (value.Value.GetType().ToString() == "Newtonsoft.Json.Linq.JArray")
                                            {

                                                string ColumnIndividual = EliminarEspacios(value.apiId.ToString());
                                                //TODO------Se elimina el parametro columnasDetalleNames y se agrega DictTablasColumnasDetalles[TableName]
                                                ColumnIndividual = BuscarElementArrayList(ColumnIndividual, DictTablasColumnasFormulariosDetalles[TableName]);
                                                //columnasDetalleNames.Add(ColumnIndividual); //TODO----Se elimina esta linea
                                                DictTablasColumnasFormulariosDetalles[TableName].Add(ColumnIndividual);

                                            }
                                            else
                                            {
                                                string ColumnIndividual = EliminarEspacios(value.apiId.ToString());
                                                //TODO------Se elimina el parametro columnasStringsNames y se agrega DictTablasColumnas[TableName]
                                                ColumnIndividual = BuscarElementArrayList(ColumnIndividual, DictTablasColumnas[TableName]);
                                                //columnasStringsNames.Add(ColumnIndividual); //TODO-----Esta linea ya no es necesaria
                                                DictTablasColumnas[TableName].Add(ColumnIndividual);
                                            }
                                        }
                                        
                                        

                                    }

                                    //Obtengo los datos de las columnas
                                    if (!Existe)
                                    {
                                        if (value.Value.GetType().ToString() != "Newtonsoft.Json.Linq.JArray")
                                        {
                                            Datos.Add(ConfigurarDato(value.Value.ToString()));
                                            //DictTablasDatos[TableName].Add(ConfigurarDato(value.Value.ToString()));
                                        }
                                        else
                                        {

                                            string columnaDetalle = EliminarEspacios(value.apiId.ToString());
                                            List<ArrayList> list = ColumnsNamesDetail(value.Value);
                                            if (list[0].Count > 0)
                                            {
                                                
                                                if (!DictTablasColumnasDetalles[TableName].ContainsKey(columnaDetalle))
                                                {
                                                    //Se agregan columnas claves a los detalles
                                                    list[0].Insert(0,"IdActividad");
                                                    DictTablasColumnasDetalles[TableName].Add(columnaDetalle, list[0]);
                                                    DictTablasDatosDetalles[TableName].Add(columnaDetalle, new ArrayList { });


                                                }

                                                foreach(ArrayList item in list[1])
                                                {
                                                    item.Insert(0,ConfigurarDato(activiesDetailVicitrack.ID.ToString()));   
                                                    DictTablasDatosDetalles[TableName][columnaDetalle].Add(item);
                                                }
                                                
                                            }
                                        }
                                    }
                                    else {

                                        // Si existe el registro y se tiene que actualizar
                                        if (Actualizar) {

                                            if (value.Value.GetType().ToString() != "Newtonsoft.Json.Linq.JArray")
                                            {
                                                DatosActualizar.Add(ConfigurarDato(value.Value.ToString()));
                                            }
                                            else
                                            {
                                                string columnaDetalle = EliminarEspacios(value.apiId.ToString());
                                                List<ArrayList> list = ColumnsNamesDetail(value.Value);
                                                if (list[0].Count > 0)
                                                {

                                                    if (!DictTablasColumnasDetalles[TableName].ContainsKey(columnaDetalle))
                                                    {
                                                        //Se agregan columnas claves a los detalles
                                                        list[0].Insert(0, "IdActividad");
                                                        DictTablasColumnasDetalles[TableName].Add(columnaDetalle, list[0]);
                                                        DictTablasDatosDetallesActualizar[TableName].Add(columnaDetalle, new ArrayList { });


                                                    }

                                                    foreach (ArrayList item in list[1])
                                                    {
                                                        item.Insert(0, ConfigurarDato(activiesDetailVicitrack.ID.ToString()));
                                                        DictTablasDatosDetallesActualizar[TableName][columnaDetalle].Add(item);
                                                    }

                                                }
                                            }

                                        }
                                    }

                                    //Fin obtener esquema de formulario
                                    j++;
                                };

                                if (Datos.Count > 7) // Datos minimos por formulario 
                                {
                                    //DatosColumnas.Add(Datos); //TODO-----Ya no se requiere esta variable
                                    DictTablasDatos[TableName].Add(Datos);
                                }

                                if (DatosActualizar.Count > 7) // Datos minimos por formulario (Activdad existente)
                                {
                                    //DatosColumnasActualizar.Add(DatosActualizar);//TODO----Ya no se requiere esta variable
                                    DictTablasDatosActualizar[TableName].Add(DatosActualizar);
                                }

                                i++; // TODO----- ya no se requiere esta variable
                            }
                        };// fin de recorrer las actividades
                    }

                    //Insertar
                    // TODO---- es necesario cambiar los arrraylist columnasStringsnames y datosColumnas
                    // por los diccionarios los cuales pueden contener varias tablas
                    if (DictTablasColumnas.Count > 0 && DictTablasDatos.Count>0)
                    {
                        // TODO-----foreach de todas las tablas a insertar
                        foreach (KeyValuePair<string, ArrayList> Dato in DictTablasDatos)
                        {
                            if (Dato.Value.Count > 0)
                            {
                                // Primer parametro es el tipo tabla en este caso es un Formulario "1"
                                bool TablaFormulario = CrearTabla(Dato.Key, DictTablasColumnas[Dato.Key]);

                                if (TablaFormulario)
                                {
                                    InsertarFormularios(Dato.Key, DictTablasColumnas[Dato.Key], Dato.Value);
                                    ActualizarFechaActualizacion(Dato.Key.Substring(1, Dato.Key.Length-1));
                                }
                            }
                        }

                        //TODO-----Foreach de todas las tablas para insertar datos detalles
                        foreach(KeyValuePair<string, Dictionary<string,ArrayList>> Tabla in DictTablasDatosDetalles)
                        {
                            foreach(KeyValuePair<string, ArrayList> Dato in Tabla.Value)
                            {
                                if (Dato.Value.Count > 0)
                                {
                                    // Primer parametro es el tipo tabla en este caso es un Formulario "1"
                                    bool TablaFormulario = CrearTabla(Tabla.Key+Dato.Key, DictTablasColumnasDetalles[Tabla.Key][Dato.Key]);
                                    if (TablaFormulario)
                                    {
                                        InsertarFormularios(Tabla.Key + Dato.Key, DictTablasColumnasDetalles[Tabla.Key][Dato.Key], Dato.Value);
                                        ActualizarFechaActualizacion(Dato.Key.Substring(1, Dato.Key.Length-1));
                                    }

                                }
                            }
                            
                        }         

                    }

                    //Actualizar
                    // TODO---- es necesario cambiar los arrraylist columnasStringsnames y datosColumnas
                    // por los diccionarios los cuales pueden contener varias tablas
                    if (DictTablasColumnas.Count > 0 && DictTablasDatosActualizar.Count > 0)
                    {
                        // TODO-----foreach de todas las tablas a actualizar
                        foreach (KeyValuePair<string, ArrayList> Dato in DictTablasDatosActualizar)
                        {
                            if (Dato.Value.Count > 0)
                            {
                                // Primer parametro es el tipo tabla en este caso es un Formulario "1"
                                bool TablaFormulario = CrearTabla(Dato.Key, DictTablasColumnas[Dato.Key]);

                                if (TablaFormulario)
                                {
                                    ModificarActividades(Dato.Key, DictTablasColumnas[Dato.Key], Dato.Value);
                                    ActualizarFechaActualizacion(Dato.Key.Substring(1, Dato.Key.Length-1));
                                }
                            }
                        }
                        //TODO-----Foreach de todas las tablas para actualizar datos detalles
                        foreach (KeyValuePair<string, Dictionary<string, ArrayList>> Tabla in DictTablasDatosDetallesActualizar)
                        {
                            foreach (KeyValuePair<string, ArrayList> Dato in Tabla.Value)
                            {
                                if (Dato.Value.Count > 0)
                                {
                                    ArrayList arrayDetalle = Dato.Value;
                                    string IdActividad = "";
                                    foreach (ArrayList itemss in arrayDetalle) {
                                        IdActividad = itemss[0].ToString();
                                        break;
                                    }

                                    bool eliminarDetalle = EliminarDetalle(Tabla.Key + Dato.Key, IdActividad);
                                    if (eliminarDetalle)
                                    {
                                        bool TablaFormulario = CrearTabla(Tabla.Key + Dato.Key, DictTablasColumnasDetalles[Tabla.Key][Dato.Key]);
                                        if (TablaFormulario)
                                        {
                                            InsertarFormularios(Tabla.Key + Dato.Key, DictTablasColumnasDetalles[Tabla.Key][Dato.Key], Dato.Value);
                                            ActualizarFechaActualizacion(Tabla.Key.Substring(1, Tabla.Key.Length-1));
                                        }
                                    }
                                   

                                }
                            }

                        }

                    }

                }
            }
            return true;

        }
        public bool ExisteConexion(List<ConexionesDisponiblesSincronizarConsultar_Result> listConexiones, string idFormulario)
        {
            bool res = false;
            foreach(ConexionesDisponiblesSincronizarConsultar_Result item in listConexiones)
            {
                if(item.GUIDFormulario == idFormulario)
                {
                    res = true;
                    break;
                }
            }

            return res;
        }

        public List<Models.ConexionesDisponiblesSincronizarConsultar_Result> ConexionesDisponiblesSincronizar(int idJob)
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };
            List<Models.ConexionesDisponiblesSincronizarConsultar_Result> lstConexionesDisponible = null;
            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {

                    lstConexionesDisponible = objMINASBDEntities.ConexionesDisponiblesSincronizarConsultar(idJob).ToList();

                    objResultado.Data = lstConexionesDisponible;

                }
            }
            catch (Exception ex)
            {
                objResultado.TipoResultado = false;
                objResultado.Mensaje = ex.Message;

            }
            return lstConexionesDisponible;
        }

        //Actualiza la fecha de actualizacion de una conexion cuando esta se ha sincronizado
        public bool ActualizarFechaActualizacion(string GuidFormulario)
        {
            Listas.Resultado objResultado = new Listas.Resultado() { Mensaje = "", TipoResultado = true };
            try
            {
                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {
                    objMINASBDEntities.ActualizarFechaConexion(GuidFormulario);
                    return true;
                }
            }
            catch (Exception ex)
            {
                objResultado.TipoResultado = false;
                objResultado.Mensaje = ex.Message;
                return false;
            }

        }
        public string ConcatenarColumnasStrings(ArrayList columnas)
        {

            return String.Join(",", columnas.ToArray());
        }

        public string ConcatenarDatosActualizarStrings(ArrayList Datos)
        {

            return String.Join("¬", Datos.ToArray());
        }

        public bool RegistroExiste(string NombreTabla, string ColumnaTabla, string Dato)
        {
            using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
            {

                List<Models.TablaExiste_Result> TablaExiste = objMINASBDEntities.TablaExiste(NombreTabla).ToList();

                if (TablaExiste.Count > 0)
                {
                    var Existe = objMINASBDEntities.RegistroExiste(NombreTabla, ColumnaTabla, ConfigurarDato(Dato)).ToList();
                    if (Existe[0].Value > 0)
                    {
                        return true;
                    }
                }


                return false;
            }
        }

        public bool ValidarRegistroActualizar(string NombreTabla, string UpdateOn, string idActividad)
        {
            using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
            {

                List<Models.TablaExiste_Result> TablaExiste = objMINASBDEntities.TablaExiste(NombreTabla).ToList();

                if (TablaExiste.Count > 0)
                {
                    var Existe = objMINASBDEntities.ValidarRegistroActualizar(NombreTabla, ConfigurarDato(UpdateOn), ConfigurarDato(idActividad)).ToList();
                    if (Existe[0].Value > 0)
                    {
                        return true;
                    }
                }


                return false;
            }
        }

        public string ConcatenarDatos(ArrayList Datos)
        {
            return String.Join("-", Datos.ToArray());
        }

        public bool InsertarFormularios(string NombreTabla, ArrayList Columnas, ArrayList Values)
        {

            using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
            {

                foreach (ArrayList PaqueteDatos in Values)
                {

                    objMINASBDEntities.AddRegistro(NombreTabla, ConcatenarColumnasStrings(PaqueteDatos), ConcatenarColumnasStrings(Columnas));

                }




            }

            return true;
        }

        public bool ModificarActividades(string NombreTabla, ArrayList Columnas, ArrayList Datos)
        {

            using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
            {

                foreach (ArrayList DatosmodificarBD in Datos)
                {

                    objMINASBDEntities.UpdateRegistro(NombreTabla, ConcatenarColumnasStrings(ConfigurarDatoModificar(DatosmodificarBD, Columnas)), ConcatenarDatosActualizarStrings(DatosmodificarBD).Split('¬')[0]);

                }
              
            }

            return true;
        }

        // TODO------Se elimino el parametro tipotabla no se requiere
        public bool CrearTabla(string NombreTabla, ArrayList Columnas)
        {
            using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
            {
                List<Models.TablaExiste_Result> lstTablaExiste = objMINASBDEntities.TablaExiste(NombreTabla).ToList();

                if (lstTablaExiste.Count == 0)
                {
                    //Tipo tabla 1 - formulario, 2- Detalle

                    /*
                    NombreTabla = "F" + NombreTabla;
                    if (tipoTabla == 2)
                    {
                        NombreTabla = "D" + NombreTabla;
                    }
                    */
                    objMINASBDEntities.CrearTabla(NombreTabla);


                    string tipoDato = "VARCHAR(MAX)";
                    string nulidad = "NULL";

                    foreach (var Columns in ConcatenarColumnasStrings(Columnas).Split(','))
                    {

                        objMINASBDEntities.AddColumna(NombreTabla, Columns, tipoDato, nulidad);

                    }


                }
                else
                { // Existe la tabla entonces se recorreran las columnas y la que no existe la crea

                    string tipoDato = "VARCHAR(MAX)";
                    string nulidad = "NULL";

                    foreach (var Columns in ConcatenarColumnasStrings(Columnas).Split(','))
                    {
                        List<Models.ColumnaExiste_Result> LstColumna = objMINASBDEntities.ColumnaExiste(NombreTabla, Columns).ToList();
                        if (LstColumna.Count == 0)
                        {
                            objMINASBDEntities.AddColumna(NombreTabla, Columns, tipoDato, nulidad);
                        }

                    }


                }
            }//Cierre de using objBD

            return true;
        }

        public string BuscarElementArrayList(string Columna, ArrayList ListaColumnas)
        {

            int iterador = 1;
            foreach (var ColumnaLista in ConcatenarColumnasStrings(ListaColumnas).Split(','))
            {

                if (ColumnaLista == Columna || ColumnaLista.Equals(Columna))
                {
                    Columna = Columna + iterador.ToString();
                    Columna = BuscarElementArrayList(Columna, ListaColumnas);
                    iterador++;

                }

            }

            return Columna;
        }

        public string EliminarEspacios(string Columna)
        {
            return Columna.Replace(" ", "");
        }

        public List<ArrayList> ColumnsNamesDetail(object detail)
        {
            ArrayList columnsNames = new ArrayList();
            ArrayList rows = new ArrayList();
            List<ArrayList> listDetail = new List<ArrayList>();
            FormValues formValue = null;
            int idDetalle = 0;
            var obj = JToken.Parse(detail.ToString());
            foreach (var item in obj.Children())
            {
                formValue = JsonConvert.DeserializeObject<FormValues>(item.ToString());
                bool canConvert = int.TryParse(formValue.apiId, out idDetalle);
                if (canConvert)
                {
                    // Guardamos los valores de una solo registro
                    ArrayList values = new ArrayList();
                    foreach (var column in JToken.Parse(formValue.Value.ToString()).Children())
                    {
                        formValue = JsonConvert.DeserializeObject<FormValues>(column.ToString());
                        // Si idDetalle es igual a 1 extraemos las columnas del detalle
                        // Guardamos los nombres de las columnas
                        if (idDetalle == 1)
                        {
                            columnsNames.Add(EliminarEspacios(formValue.apiId.ToString()));
                        }
                        // Guardamos los registros del detalle
                        values.Add(ConfigurarDato(formValue.Value.ToString()));
                    }

                    // Añadimos los valores al array de filas
                    rows.Add(values);


                }
                else
                {
                    // Guardamos los nombres de las columnas
                    foreach (var column in JToken.Parse(formValue.Value.ToString()).Children())
                    {
                        formValue = JsonConvert.DeserializeObject<FormValues>(column.ToString());
                        columnsNames.Add(formValue.apiId);
                    }
                }



            }
            // Guardamos las lista que contiene las columnas y registros
            listDetail.Add(columnsNames);
            listDetail.Add(rows);
            return listDetail;
        }

        public string ConfigurarDato(string Dato)
        {
            string DatoConfigurado = Dato;
            if ((Dato.Equals("") || string.IsNullOrEmpty(Dato)))
            {
                DatoConfigurado = "''";
            }
            else
            {
                DatoConfigurado = "'" + Dato + "'";
            }


            return DatoConfigurado;

        }

        public Boolean EliminarDetalle(string NombreTabla, string IdActividad) {
     

                try {

                using (Models.minasDBEntities objMINASBDEntities = new Models.minasDBEntities())
                {
                    string ColumnaTabla = "IdActividad";

                    var Existe = objMINASBDEntities.RegistroExiste(NombreTabla, ColumnaTabla, IdActividad).ToList();
                    if (Existe[0].Value > 0)
                    {
                        //Se verifica que existan datos y se eliminan
                        objMINASBDEntities.EliminarDetalleActividad(NombreTabla, IdActividad);

                    }

                }//Cierre de using
                return true;    

                }
                catch (Exception ex) {
                    return false;
                }

           

        }

        public ArrayList ConfigurarDatoModificar(ArrayList Datos, ArrayList Columnas)
        {
            ArrayList DatosModificar = new ArrayList();
            string DatoConfiguradoModificar = "";
            int i = 0;
            string[] Values = ConcatenarDatosActualizarStrings(Datos).Split('¬');
            foreach (var ColumnName in Columnas)
            { 
                DatoConfiguradoModificar += ColumnName + "=" + Values[i];

                DatosModificar.Add(DatoConfiguradoModificar);
                DatoConfiguradoModificar = "";
                i++;
            }


            return DatosModificar;

        }

        public bool get(int idJob)
        {
            helper = new HelperService();
            authService = new AuthService(helper.getConfig("USER_VICITRACK"), helper.getConfig("PASSWORD_VICITRACK"));

            return main(idJob);
        }

    }
}

/* 
 
     Respaldo



     foreach (Activity ele in activiesVicitrack)
                        {
                            DetailActivity activiesDetailVicitrack = JsonConvert.DeserializeObject<DetailActivity>(activityService.getDetailActivity(ele.GUID));
                            ResponseModel responseFindForm = JsonConvert.DeserializeObject<ResponseModel>(connectionService.findConnectionGUIDForms(activiesDetailVicitrack.FormGUID));

                            if (responseFindForm.value.ToString() != "[]")
                            {
                                j = 0;
                                ArrayList Datos = new ArrayList();
                                foreach(var value in activiesDetailVicitrack.Values)
                                {
                                    FormGuid = activiesDetailVicitrack.FormGUID;
                                    TableName = "F" + FormGuid;

                                    if (!RegistroExiste(TableName, ColumnaActividadExiste, activiesDetailVicitrack.ID.ToString()))
                                    {
                                        Existe = false;
                                    }
                                    else
                                    {
                                        //Existe el registro inv

                                        if (ValidarRegistroActualizar(TableName, activiesDetailVicitrack.UpdatedOn.ToString(), activiesDetailVicitrack.ID.ToString()) && )
                                        {



                                        }
                                        else {
                                            Actualizar = false;
                                        }






                                        //Cierre inv




                                        if (i != 0)
                                        {
                                            //Codigo para extaer las cadenas de actualización
                                            break;
                                        }

                                        Existe = true;
                                    }



                                    if (j == 0)
                                    {

                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.ID.ToString()));                                  
                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.Title.ToString()));
                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.LocationName.ToString()));
                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.LocationGUID.ToString()));
                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.CreatedOn.ToString()));
                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.UpdatedOn.ToString()));
                                        Datos.Add(ConfigurarDato(activiesDetailVicitrack.UserName.ToString()));
                                      
                                    }

                                    //Obtener la estructura de los formulario
                                    if (i == 0)
                                    {
                                        if (!BandIDColumn)
                                        {
                                            columnasStringsNames.Add("ID");
                                            columnasStringsNames.Add("Title");
                                            columnasStringsNames.Add("LocationName");
                                            columnasStringsNames.Add("LocationGUID");
                                            columnasStringsNames.Add("CreatedOn");
                                            columnasStringsNames.Add("UpdatedOn");
                                            columnasStringsNames.Add("UserName");

                                            BandIDColumn = true;

                                 
                                        }



                                        if (value.Value.GetType().ToString() == "Newtonsoft.Json.Linq.JArray")
                                        {
                                            string ColumnIndividual = EliminarEspacios(value.apiId.ToString());
                                            ColumnIndividual = BuscarElementArrayList(ColumnIndividual, columnasDetalleNames);
                                            columnasDetalleNames.Add(ColumnIndividual);
                                        }
                                        else
                                        {
                                            string ColumnIndividual = EliminarEspacios(value.apiId.ToString());
                                            ColumnIndividual = BuscarElementArrayList(ColumnIndividual, columnasStringsNames);
                                            columnasStringsNames.Add(ColumnIndividual);
                                        }

                                    }


                                    //Obtengo los datos de las columnas


                                    if (!Existe)
                                    {
                                        if (value.Value.GetType().ToString() != "Newtonsoft.Json.Linq.JArray")
                                        {
                                            Datos.Add(ConfigurarDato(value.Value.ToString()));
                                        }
                                    }


                                    //Fin obtener esquema de formulario
                                    j++;
                                };

                                if (Datos.Count > 7) // Datos minimos por formulario
                                {
                                    DatosColumnas.Add(Datos);
                                }
                                i++;
                            }
                        };// fin de recorrer las actividades
     
     */
