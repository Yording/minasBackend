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
    public class UserService
    {
        // Atributos
        private static HelperService helper;
        private static string _api = string.Empty;
        private static string _apiUsers = "Users/All?AccessToken={0}";
        private string _token = string.Empty;
        private static readonly HttpClient client = new HttpClient();
        private static string _apiOdata = string.Empty;

        public UserService(string token)
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

        public string getUsers()
        {
            return _createRequest(_api + string.Format(_apiUsers, this._token));

        }

        public string findUserId(string Id)
        {
            return _createRequest(string.Format("{0}Usuarios?$filter=GUIDUsuario eq '{1}'", _apiOdata, Id));
        }
        public async void createUser(string Id, string nombre, string apellido)
        {
            var values = new Dictionary<string, string>
            {
               { "GUIDUsuario", Id },
               { "nombre", nombre },
               { "apellido", apellido }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(_apiOdata + "Usuarios", content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

        }
    }
}