using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppMinas.Services
{
    class ActivityService
    {

        // Atributos
        private static HelperService helper;
        private static string _api = string.Empty;
        private static string _apiActivities = "Surveys/Activities?AccessToken={0}&From={1:yyyy-MM-dd}&To={2:yyyy-MM-dd}";
        private static string _apiDetailActivities = "Surveys/Activity?AccessToken={0}&GUID={1}&ListValues=true";
        private string _token = string.Empty;

        public ActivityService(string token)
        {
            this._token = token;
            helper = new HelperService();
            _api = helper.getConfig("API_VICITRACK");
        }

        public string getActivity()
        {
            DateTime FechaInicial = DateTime.Now;
            DateTime FechaFinal = FechaInicial.AddDays(1);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_api + string.Format(_apiActivities, this._token,FechaInicial,FechaFinal));
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
        public string getDetailActivity(string GUID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_api + string.Format(_apiDetailActivities, this._token, GUID));
            request.Timeout = 300000;
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
                using (Stream responseStreams = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStreams, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
