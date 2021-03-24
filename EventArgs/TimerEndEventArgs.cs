using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer
{
    public class TimerEndEventArgs : EventArgs
    {
        public TimerEndEventArgs(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
