using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer
{
    public class TimerStartEventArgs : EventArgs
    {
        public TimerStartEventArgs(int time, string name)
        {
            Name = name;
            Time = time / 1000;
        }
        public string Name { get; }
        public int Time { get; }
    }
}
