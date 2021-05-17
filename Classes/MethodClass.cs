using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork_Delegates_Timer.Classes
{
    public class MethodClass : ICountDownNotifier
    {
        public bool Subscribed { get; private set; } = false;
        
        public event EventHandler<int> TimerStartEvent;
        
        void ICountDownNotifier.Init(CountDownTimer timer)
        {
            if (!Subscribed)
            {
                timer.TimerStartEvent += OnTimerStart;
                timer.TimeLeftEvent += OnTimeLeft;
                timer.TimerEndEvent += OnTimerEnd;
                timer.UnsubscribeEvent += Unsubscribe;
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
            Console.WriteLine($"Метод: Таймер {e.Name} достиг нуля.\n");
        }

        private void Unsubscribe(CountDownTimer timer)
        {
            TimerStartEvent -= timer.SetupTimer;
            Subscribed = false;
        }
    }
}
