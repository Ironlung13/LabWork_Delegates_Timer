using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer
{
    public class TimeLeftEventArgs : EventArgs
    {
        public TimeLeftEventArgs(int time, string name)
        {
            Time = time / 1000;
            Name = name;
        }
        public string Name { get; }
        public int Time { get; }
    }
}
