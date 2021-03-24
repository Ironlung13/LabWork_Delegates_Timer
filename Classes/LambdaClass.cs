using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer.Classes
{
    public class LambdaClass : ICountDownNotifier
    {
        public bool Subscribed { get; private set; } = false;
        public event EventHandler<int> TimerStartEvent;
        void ICountDownNotifier.Init(CountDownTimer timer)
        {
            if (!Subscribed)
            {
                timer.TimerStartEvent += (sender, e) => { Console.WriteLine($"Лямбда: Таймер {e.Name} запущен. Время ожидания: {e.Time} секунд."); };
                timer.TimeLeftEvent += (sender, e) => { Console.WriteLine($"Лямбда: На таймере {e.Name} осталось {e.Time} секунд."); };
                timer.TimerEndEvent += (sender, e) => { Console.WriteLine($"Лямбда: Таймер {e.Name} достиг нуля."); };
                timer.UnsubscribeEvent += (timer) => { TimerStartEvent -= timer.SetupTimer; Subscribed = false; };
                TimerStartEvent += timer.SetupTimer;

                Subscribed = true;
            }
            else
            {
                throw new InvalidOperationException("Already subscribed to timer.");
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
