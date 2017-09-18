using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppMinas.Services;

namespace AppMinas.Services
{
    class AuthService
    {
        // Atributos
        private static HelperService helper;
        private static string _api = string.Empty;
        private static string _apiToken = "Users/Authentication?Login={0}&Password={1}";
        private string _apiUser = string.Empty;
        private string _apiPassword = string.Empty;

        public AuthService(string user, string password)
        {
            this._apiUser = user;
            this._apiPassword = password;
            helper = new HelperService();
            _api = helper.getConfig("API_VICITRACK");
        }
        public String getAuthentication()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_api + string.Format(_apiToken, this._apiUser, this._apiPassword));
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
    }
}
