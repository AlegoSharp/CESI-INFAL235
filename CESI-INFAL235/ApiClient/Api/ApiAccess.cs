using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApiClient.Api
{
    public class ApiAccess
    {
        private HttpClient httpClient = new HttpClient();
        private ApiAccess()
        {

        }
        public static ApiAccess GetInstance(Window caller)
        {
            return new ApiAccess(); ;
        }

        public async Task<string> GetRoute(string routeName)
        {
            //Create an HTTP client object
            httpClient = new HttpClient();

            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;

            //The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
            //especially if the header value is coming from user input.
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            // 0 = IP / 1 = PORT / 2 = ROUTE
            string apiadr = Properties.Settings.Default.API_LOCAL_ADR + ":" + Properties.Settings.Default.API_LOCAL_ADR;
            Uri requestUri = new Uri(string.Format("http://{0}/api/{1}", apiadr,routeName));

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return httpResponseBody;
        }
    }
}
