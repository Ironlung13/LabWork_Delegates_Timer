using System;
using LabWork_Delegates_Timer.Classes;

namespace LabWork_Delegates_Timer
{
    class Program
    {
        static void Main()
        {
            ICountDownNotifier[] array = new ICountDownNotifier[] { new MethodClass(), new AnonymousClass(), new LambdaClass() };
            CountDownTimer[] timers = new[] { new CountDownTimer("Чтение задания"), 
                                              new CountDownTimer("Выполнение задания"), 
                                              new CountDownTimer("Проверка задания перед отправкой") };
            for (int i = 0; i < timers.Length; i++)
            {
                array[i].Init(timers[i]);
                try
                {
                    array[i].Run(5);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Timer overflow. Skipping Timer.");
                }
            }
            foreach(var timer in timers)
            {
                timer.UnsubscribeAll();
            }

            try
            {
                array[1].Run(3);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
