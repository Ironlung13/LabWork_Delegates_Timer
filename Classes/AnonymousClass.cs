using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer.Classes
{
    public class AnonymousClass : ICountDownNotifier
    {
        public bool Subscribed { get; private set; } = false;
        
        public event EventHandler<int> TimerStartEvent;
        
        void ICountDownNotifier.Init(CountDownTimer timer)
        {
            if (!Subscribed)
            {
                timer.TimerStartEvent += delegate (object sender, TimerStartEventArgs e) { Console.WriteLine($"Анонимный: Таймер {e.Name} запущен. Время ожидания: {e.Time} секунд."); };
                timer.TimeLeftEvent += delegate (object sender, TimeLeftEventArgs e) { Console.WriteLine($"Анонимный: На таймере {e.Name} осталось {e.Time} секунд."); };
                timer.TimerEndEvent += delegate (object sender, TimerEndEventArgs e) { Console.WriteLine($"Анонимный: Таймер {e.Name} достиг нуля.\n"); };
                timer.UnsubscribeEvent += delegate (CountDownTimer timer) { TimerStartEvent -= timer.SetupTimer; Subscribed = false; };
                TimerStartEvent += timer.SetupTimer;

                Subscribed = true;
            }
            else
            {
                throw new InvalidOperationException("Already subscribed to a timer.");
            }
        }
        
        void ICountDownNotifier.Run(int time)
        {
            if (time < 0)
            {
                throw new ArgumentException("Can't setup timer using negative time value.");
            }
            if (Subscribed)
            {
                TimerStartEvent(this, time);
            }
            else
            {
                throw new InvalidOperationException("Not subscribed to a timer.");
            }
        }
    }
}
