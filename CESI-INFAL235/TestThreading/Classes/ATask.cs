using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestThreading.Classes
{
    public delegate void Notify(TaskResult tr);  // delegate
    public delegate void NotifyMs(double value);  // delegate

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

        public event Notify ProcessCompleted; // event
        public event NotifyMs TaskCompleted; // event

        public ATask(int numberOfTasks = 1000)
        {
            NumberOfTasks = numberOfTasks;
            TaskCompleted += ATask_TaskCompleted;
        }

        private void ATask_TaskCompleted(double value)
        {
            TotalMs += value;
        }

        protected virtual void OnProcessCompleted(TaskResult tr) //protected virtual method
        {
            ProcessCompleted?.Invoke(tr);
        }
        protected virtual void OnTaskCompleted(double value) //protected virtual method
        {
            TaskCompleted?.Invoke(value);
        }
        public double RunPoolOfTasks(int taskParThread = 5,bool withSleep = true)
        {
            Start = DateTime.Now;
            Thread th = null;
            for(int i = 0; i < (NumberOfTasks / taskParThread); i++)
            {
                TaskResult tr = new TaskResult();
                tr.SetStart(DateTime.Now);

                new Thread(() => 
                {
                    for(int j = 0; j < taskParThread; j++)
                    {
                        RunNewTaskSync();
                    }
                    tr.SetEnd(DateTime.Now);
                    //TotalMs += (tr.End - tr.Start).TotalMilliseconds;
                    OnProcessCompleted(tr);
                    OnTaskCompleted((tr.End - tr.Start).TotalMilliseconds);
                }).Start();

                Thread.Sleep(40);
            }

            //while (Compteur < NumberOfTasks)
            //{
            //    if (withSleep)
            //    {
            //        Thread.Sleep(10);
            //    }
            //}

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

            //while (Compteur < NumberOfTasks)
            //{
            //    Thread.Sleep(10);
            //}
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
                    //TotalMs += (tr.End - tr.Start).TotalMilliseconds;
                    OnProcessCompleted(tr);
                    OnTaskCompleted((tr.End - tr.Start).TotalMilliseconds);
                }
                catch (Exception ex)
                {
                    Errors++;
                }
                Compteur++;

            }).Start();
        }

        public void RunNewTaskSync()
        {

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
