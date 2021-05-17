using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LabWork_Delegates_Timer
{
    public class CountDownTimer
    {
        public CountDownTimer(string name)
        {
            Name = name;
        }
        
        private int _time;
        
        public bool Running { get; private set; }
        public string Name { get; set; }

        public event EventHandler<TimerStartEventArgs> TimerStartEvent;
        public event EventHandler<TimeLeftEventArgs> TimeLeftEvent;
        public event EventHandler<TimerEndEventArgs> TimerEndEvent;
        public event Action<CountDownTimer> UnsubscribeEvent;
        
        public void SetupTimer(object sender, int time)
        {
            if (!Running)
            {
                if (time < 0)
                {
                    throw new ArgumentException("Can't setup timer using negative time value.");
                }

                try
                {
                    _time = checked(time * 1000);
                }
                catch (OverflowException)
                {
                    throw new OverflowException("Time value overflow.");
                }
                
                StartTimer();
            }
        }
        
        public void UnsubscribeAll()
        {
            TimerStartEvent = null;
            TimerEndEvent = null;
            TimeLeftEvent = null;
            if (!(UnsubscribeEvent is null))
            {
                UnsubscribeEvent(this);
            }
        }
        
        private void StartTimer()
        {
            TimerStartEvent(this, new TimerStartEventArgs(_time, Name));
            //Чтобы постоянно не создавать новые объекты класса TimeLeftEventArgs
            TimeLeftEventArgs info = new TimeLeftEventArgs(_time, Name);
            while (_time > 0)
            {
                info.Time = _time;
                TimeLeftEvent(this, info);
                Thread.Sleep(1000);
                _time -= 1000;
            }
            TimerEndEvent(this, new TimerEndEventArgs(Name));
        }
    }
}
