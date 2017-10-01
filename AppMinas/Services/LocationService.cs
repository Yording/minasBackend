using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace AppMinas.Services
{
    public class LocationService
    {
        // Atributos
        private static HelperService helper;
        private static string _api = string.Empty;
        private static string _apiLocations = "Surveys/Forms?AccessToken={0}";
        private static string _apiTypepoLocations = "Locations/Types?AccessToken={0}";
        private string _token = string.Empty;
        private static readonly HttpClient client = new HttpClient();
        private static string _apiOdata = string.Empty;

        public LocationService(string token)
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

        public string getTypeLocations()
        {
            return _createRequest(_api + string.Format(_apiTypepoLocations, this._token));

        }

        public string findTypeLocationGUID(string GUID)
        {
            return _createRequest(string.Format("{0}TipoLocaciones?$filter=GUIDTipoLocation eq '{1}'", _apiOdata, GUID));
        }
        public async void createTypeLocation(string GUID, string nombre,bool estado)
        {
            var values = new Dictionary<string, string>
            {
               { "GUIDTipoLocation", GUID },
               { "nombre", nombre },
               { "estado", estado.ToString() }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(_apiOdata + "TipoLocaciones", content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

        }
    }
}