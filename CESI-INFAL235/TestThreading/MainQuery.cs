using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestThreading.Classes;

namespace TestThreading
{
    class MainQuery
    {
        static void Main(string[] args)
        {
            string arg1 = "";
            if (args.Length > 0)
            {
                arg1 = args[0];

            }
            else
            {
                arg1 = Console.ReadLine();
            }
            switch (arg1)
            {
                case "0":
                    SingleThread(); break;

                case "1":
                    MultiThreadAuCarre(); break;

                case "2":
                    MultiThread(); break;

                case "3":
                    MultiTaskMultiThreadFive(); break;

                case "4":
                    MultiTaskMultiThreadTen(); break;
            }
        }

        public static void SingleThread()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(1000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }
        public static void MultiThreadAuCarre()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(true);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }

        public static void MultiThread()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(false);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }
        public static void MultiTaskMultiThreadFive()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(5);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }

        public static void MultiTaskMultiThreadTen()
        {
            string adr = Properties.Api.Default.Adresse;
            ATask a = new ATask();
            Program p = new Program();
            p.httpClient = new HttpClient();
            p.requestUri = new Uri(adr);


            a.Action = p.CallDate;
            var tasks = a.RunPoolOfTasks(10);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Minutes : " + (a.Start - a.End).Minutes);
            Console.WriteLine("Secondes : " + (a.Start - a.End).Seconds);
            Console.WriteLine("Millisecondes : " + (a.Start - a.End).Milliseconds);
            Console.ReadLine();
        }

    }
}
