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
        public Action Action { get; set; }
        public int NumberOfTasks { get; set; }
        public int Errors { get; set; }
        public int Compteur { get; set; }
        public double TotalMs { get; set; }
        public bool MoreMultiThreading { get; set; }
        public int LeftPosition{ get; set; }

        public ATask(int numberOfTasks = 1000)
        {
            NumberOfTasks = numberOfTasks;
        }

        public double RunPoolOfTasks(int taskParThread = 5,bool withSleep = true)
        {
            Start = DateTime.Now;
            Thread th = null;
            for(int i = 0; i < (NumberOfTasks / taskParThread); i++)
            {
                new Thread(() => 
                {
                    for(int j = 0; j < taskParThread; j++)
                    {
                        RunNewTaskSync();
                    }
                }).Start();
                Thread.Sleep(40);
            }

            while (Compteur < NumberOfTasks)
            {
                if (withSleep)
                {
                    Thread.Sleep(10);
                }
            }

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
                    Errors++;
                }
                try
                {
                    tr.SetEnd(DateTime.Now);
                    TotalMs += (tr.End - tr.Start).TotalMilliseconds;
                    Compteur++;
                }
                catch (Exception ex)
                {
                    Errors++;
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
                Errors++;
            }

        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
