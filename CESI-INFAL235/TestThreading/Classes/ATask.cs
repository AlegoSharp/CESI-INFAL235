using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestThreading.Classes
{
    public class ATask : IDisposable
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        List<TaskResult> Tasks { get; set; }
        public Action Action { get; set; }
        public int NumberOfTasks { get; set; }
        public int Errors { get; set; }
        public int Compteur { get; set; }
        public double TotalMs { get; set; }
        public bool MoreMultiThreading { get; set; }
        public ATask(int numberOfTasks = 1000)
        {
            Tasks = new List<TaskResult>();
            NumberOfTasks = numberOfTasks;
        }

        public double RunPoolOfTasks(int taskParThread = 5,bool withSleep = true)
        {
            Start = DateTime.Now;
            Thread th = null;
            for(int i = 0; i < (NumberOfTasks / taskParThread); i++)
            {
                th = new Thread(() => 
                {
                    for(int j = 0; j < taskParThread; j++)
                    {
                        RunNewTaskSync();
                    }
                });
                th.Start();
                Thread.Sleep(50);
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("NB TASK DONE : " + Compteur);

            }

            while (Compteur < NumberOfTasks)
            {
                if (withSleep)
                {
                    Thread.Sleep(10);
                }
            }
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("NB TASK DONE : " + Compteur);
            End = DateTime.Now;

            return TotalMs / Compteur;
        }

        public double RunPoolOfTasks(bool moreMultiThreading = false)
        {
            Start = DateTime.Now;
            for (int i = 0; i < NumberOfTasks; i++)
            {

                if (moreMultiThreading)
                {
                    new Task(() => RunNewTask()).Start();
                }
                else
                {
                    RunNewTask();
                }
                Thread.Sleep(50);
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("NB TASK DONE : " + Compteur);
            }

            while (Compteur < NumberOfTasks)
            {
                Thread.Sleep(10);
            }
            End = DateTime.Now;

            return TotalMs /Compteur;
        }

        public void RunNewTask()
        {
            TaskResult tr = new TaskResult();
            tr.SetStart(DateTime.Now);
            new Thread(() => {
                try
                {
                    Action.Invoke();
                }
                catch(Exception ex)
                {
                    Console.SetCursorPosition(0, 21);

                    Console.WriteLine(ex.Message);
                    Errors++;
                }
                try
                {
                    Console.SetCursorPosition(0, 20);

                    tr.SetEnd(DateTime.Now);
                    TotalMs += (tr.End - tr.Start).TotalMilliseconds;
                    Compteur++;
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(0, 21);

                    Errors++;
                    Console.WriteLine(ex.Message);

                }

            }).Start();
        }

        public void RunNewTaskSync()
        {
            TaskResult tr = new TaskResult();
            tr.SetStart(DateTime.Now);
            try
            {

                Action.Invoke();
            }
            catch (Exception ex)
            {
                Errors++;
            }
            try
            {
                tr.SetEnd(DateTime.Now);
                TotalMs += (tr.End - tr.Start).TotalMilliseconds;
                Compteur++;
            }
            catch(Exception ex)
            {
                Console.SetCursorPosition(0, 21);
                Errors++;
                Console.WriteLine(ex.Message);
            }

        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
