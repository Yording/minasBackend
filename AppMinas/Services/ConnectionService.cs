using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppMinas.Services
{
    class ConnectionService
    {
        // Atributos
        private static HelperService helper;
        private static string _api = string.Empty;
        private static string _apiForms = "Surveys/Forms?AccessToken={0}";
        private string _token = string.Empty;
        private static readonly HttpClient client = new HttpClient();
        private static string _apiOdata = string.Empty;

        public ConnectionService(string token="")
        {
            this._token = token;
            helper = new HelperService();
            _api = helper.getConfig("API_VICITRACK");
            _apiOdata = helper.getConfig("API_ODATA");
            client.BaseAddress = new Uri(_apiOdata);
        }

        private string _createRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        public string getConnectionsForms()
        {
            return _createRequest(_apiOdata + "/Conexiones?$expand=Formulario,TipoConexion&$filter=TipoConexion/nombreTipoConexion eq 'Formulario'");
        }

        public string findConnectionGUIDForms(string GUID)
        {
            return _createRequest(string.Format("{0}/Conexiones?$expand=Formulario&$select=Formulario/GUIDFormulario&$filter=Formulario/GUIDFormulario eq '{1}'", _apiOdata,GUID));
        }
    }
}
