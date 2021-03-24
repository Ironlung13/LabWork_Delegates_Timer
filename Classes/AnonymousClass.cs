using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer.Classes
{
    public class AnonymousClass : ICountDownNotifier
    {
        public event EventHandler<int> TimerStartEvent;
        void ICountDownNotifier.Init(CountDownTimer timer)
        {
            timer.TimerStartEvent += delegate(object sender, TimerStartEventArgs e) { Console.WriteLine($"Анонимный: Таймер {e.Name} запущен. Время ожидания: {e.Time} секунд."); };
            timer.TimeLeftEvent += delegate (object sender, TimeLeftEventArgs e) { Console.WriteLine($"Анонимный: На таймере {e.Name} осталось {e.Time} секунд."); };
            timer.TimerEndEvent += delegate (object sender, TimerEndEventArgs e) { Console.WriteLine($"Анонимный: Таймер {e.Name} достиг нуля."); };
            TimerStartEvent += timer.SetupTimer;
        }
        void ICountDownNotifier.Run(int time)
        {
            if (time < 0)
            {
                throw new ArgumentException("Can't setup timer using negative time value.");
            }
            TimerStartEvent(this, time);
        }
    }
}
