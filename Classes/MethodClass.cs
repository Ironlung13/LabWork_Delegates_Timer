using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer.Classes
{
    public class MethodClass : ICountDownNotifier
    {
        public event EventHandler<int> TimerStartEvent;
        void ICountDownNotifier.Init(CountDownTimer timer)
        {
            timer.TimerStartEvent += OnTimerStart;
            timer.TimeLeftEvent += OnTimeLeft;
            timer.TimerEndEvent += OnTimerEnd;
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
        private void OnTimerStart(object sender, TimerStartEventArgs e)
        {
            Console.WriteLine($"Метод: Таймер {e.Name} запущен. Время ожидания: {e.Time} секунд.");
        }

        private void OnTimeLeft(object sender, TimeLeftEventArgs e)
        {
            Console.WriteLine($"Метод: На таймере {e.Name} осталось {e.Time} секунд.");
        }

        private void OnTimerEnd(object sender, TimerEndEventArgs e)
        {
            Console.WriteLine($"Метод: Таймер {e.Name} достиг нуля.");
        }
    }
}
