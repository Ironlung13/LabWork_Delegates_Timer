using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer.Classes
{
    public class LambdaClass : ICountDownNotifier
    {
        public event EventHandler<int> TimerStartEvent;
        void ICountDownNotifier.Init(CountDownTimer timer)
        {
            timer.TimerStartEvent += (sender, e) => { Console.WriteLine($"Лямбда: Таймер {e.Name} запущен. Время ожидания: {e.Time} секунд."); };
            timer.TimeLeftEvent += (sender, e) => { Console.WriteLine($"Лямбда: На таймере {e.Name} осталось {e.Time} секунд."); };
            timer.TimerEndEvent += (sender, e) => { Console.WriteLine($"Лямбда: Таймер {e.Name} достиг нуля."); };
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
