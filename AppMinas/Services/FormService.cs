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
    class FormService
    {
        // Atributos
        private static HelperService helper;
        private static string _api = string.Empty;
        private static string _apiForms = "Surveys/Forms?AccessToken={0}";
        private string _token = string.Empty;
        private static readonly HttpClient client = new HttpClient();
        private static string _apiOdata = string.Empty;

        public FormService(string token)
        {
            this._token = token;
            helper = new HelperService();
            _api = helper.getConfig("API_VICITRACK");
            _apiOdata = helper.getConfig("API_ODATA");
            //client.BaseAddress = new Uri(_apiOdata);
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

        public string getForms()
        {
            return _createRequest(_api + string.Format(_apiForms, this._token));
            
        }

        public string findFormGUID(string GUID)
        {
            return _createRequest(string.Format("{0}Formularios?$filter=GUIDFormulario eq '{1}'", _apiOdata, GUID));
        }
        public async void createForm(string GUID, string nombre)
        {
            var values = new Dictionary<string, string>
            {
               { "GUIDFormulario", GUID },
               { "nombreFormulario", nombre },
               { "estado", "true" }
            };
            
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(_apiOdata + "Formularios", content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

        }
    }
}
