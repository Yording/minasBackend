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


        public bool main()
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

                    int i = 0; // Actividades
                    int j = 0; //Campos de Actividades
                    string FormGuid = "";
                    bool BandIDColumn = false;
                    ArrayList columnasStringsNames = new ArrayList();
                    ArrayList columnasDetalleNames = new ArrayList();

                    //ArrayList Datos = new ArrayList(); // en este array almaceno los datos individuales
                    ArrayList DatosColumnas = new ArrayList(); // en este array meto todos los Formularios que se encuentran en Datos
                    ArrayList DatosColumnasActualizar = new ArrayList(); // en este array meto todos los Formularios que se encuentran en Datos donde se encuentre la validacion para actualizar
                    ArrayList DatosDetalles = new ArrayList();

                    string TableName = "";
                    string ColumnaActividadExiste = "ID";
                    bool Existe = false;
                    bool Actualizar = false;

                    //Validar si existen conexiones
                    if (connectionsForms.Count > 0)
                    {
                        // Foreach de todos las actividades(registros en los formularios)
                        foreach (Activity ele in activiesVicitrack)
                        {
                            DetailActivity activiesDetailVicitrack = JsonConvert.DeserializeObject<DetailActivity>(activityService.getDetailActivity(ele.GUID));
                            ResponseModel responseFindForm = JsonConvert.DeserializeObject<ResponseModel>(connectionService.findConnectionGUIDForms(activiesDetailVicitrack.FormGUID));

                            if (responseFindForm.value.ToString() != "[]")
                            {
                                j = 0;
                                ArrayList Datos = new ArrayList();
                                ArrayList DatosActualizar = new ArrayList();
                                foreach (var value in activiesDetailVicitrack.Values)
                                {
                                   
                                   if (j == 0)
                                    {
                                        //Solo vamos a entrar en este fragmento de codigo en el primer recorrido de datos de cada actividad
                                        FormGuid = activiesDetailVicitrack.FormGUID;
                                        TableName = "F" + FormGuid;


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
                                    else {

                                        // Si existe el registro y se tiene que actualizar
                                        if (Actualizar) {

                                            if (value.Value.GetType().ToString() != "Newtonsoft.Json.Linq.JArray")
                                            {
                                                DatosActualizar.Add(ConfigurarDato(value.Value.ToString()));
                                            }

                                        }
                                    }

                                    //Fin obtener esquema de formulario
                                    j++;
                                };

                                if (Datos.Count > 7) // Datos minimos por formulario 
                                {
                                    DatosColumnas.Add(Datos);
                                }

                                if (DatosActualizar.Count > 7) // Datos minimos por formulario (Activdad existente)
                                {
                                    DatosColumnasActualizar.Add(DatosActualizar);
                                }

                                i++;
                            }
                        };// fin de recorrer las actividades
                    }

                    //Insertar
                    if (columnasStringsNames.Count > 0 && DatosColumnas.Count>0)
                    {

                        // Primer parametro es el tipo tabla en este caso es un Formulario "1"
                        bool TablaFormulario = CrearTabla(1, TableName, columnasStringsNames);

                        if (TablaFormulario)
                        {
                            InsertarFormularios(TableName, columnasStringsNames, DatosColumnas);
                        }

                    }

                    //Actualizar
                    if (columnasStringsNames.Count > 0 && DatosColumnasActualizar.Count > 0)
                    {


                        bool TablaFormulario = CrearTabla(1, TableName, columnasStringsNames);

                        if (TablaFormulario)
                        {
                            ModificarActividades(TableName, columnasStringsNames, DatosColumnasActualizar);
                        }

                    }




                    /*
                    if (columnasDetalleNames.Count > 0)
                    {
                        bool TablaDetalle = CrearTabla(2, "D" + FormGuid, columnasDetalleNames);
                        if (TablaDetalle)
                        {
                            //metodo insertar
                            insertarFormularios("D" + FormGuid, columnasStringsNames, DatosColumnas);
                        }
                    }

                    */
                }
            }
            return true;

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

        public bool CrearTabla(int tipoTabla, string NombreTabla, ArrayList Columnas)
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

        public ArrayList ColumnsNamesDetail(object detail)
        {
            ArrayList columnsNames = new ArrayList();
            ArrayList rows = new ArrayList();
            ArrayList listDetail = new ArrayList();
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
                            columnsNames.Add(formValue.apiId);
                        }
                        // Guardamos los registros del detalle
                        values.Add(formValue.Value.ToString());
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

        public bool get()
        {
            helper = new HelperService();
            authService = new AuthService(helper.getConfig("USER_VICITRACK"), helper.getConfig("PASSWORD_VICITRACK"));

            return main();
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
