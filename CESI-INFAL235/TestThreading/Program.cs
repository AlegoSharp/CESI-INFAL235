using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestThreading
{
    public class Program
    {

        public HttpClient httpClient;
        public Uri requestUri;
        public List<string> values;
        static void Mainz(string[] args)
        {
            Console.WriteLine("running");
            string adr = Properties.Api.Default.Adresse;
            Program p = new Program();

            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);
            p.values = new List<string>();
            DateTime start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                new Thread(() =>
                {
                    Program.GetDate();
                }).Start();
            }
            //var sw = Stopwatch.StartNew();
            //while (sw.Elapsed.TotalSeconds < 60)
            //{
            //    new Task(() =>
            //    {
            //        p.CallDate(adr);
            //    }).Start();
            //}
            TimeSpan end = DateTime.Now - start;
            Console.WriteLine(end.Minutes + " : " + end.Seconds + " : " + end.Milliseconds);
            Console.WriteLine(p.values.Count);
            Console.WriteLine("Enter to continue");
            Console.Read();
            //File.WriteAllLines(Guid.NewGuid().ToString() + ".txt", p.values.ToArray());
        }

        public static async void GetDate()
        {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create("https://localhost:44343/api/e5");
            WebResponse response = http.GetResponse();
            //Stream stream = response.GetResponseStream();
            //StreamReader sr = new StreamReader(stream);
            //string content = sr.ReadToEnd();
            //Console.WriteLine(content);
        }
        public async void CallDate()
        {
            string httpResponseBody = "";
            try
            {
                httpResponseBody = await httpClient.GetStringAsync(requestUri);
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            //Console.WriteLine(httpResponseBody);
            
        }

    }
}
