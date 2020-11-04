using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestThreading.Classes
{
    public class TaskResult
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public void SetStart(DateTime start)
        {
            Start = start;
        }

        public void SetEnd(DateTime end)
        {
            End = end;
        }
    }
}
