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
                                              new CountDownTimer("Проверка задания") };
            //Ввод длительности таймеров
            int[] time = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Введите продолжительность {i + 1}го таймера в секундах:");
                //Ввод чисел. Отбрасывает отрицательные значения, и значения, которые приведут к overflow
                while (!int.TryParse(Console.ReadLine(), out time[i]) || time[i] < 0 || time[i] >= int.MaxValue / 1000)
                {
                    Console.WriteLine("Неверный ввод");
                }
            }
            //Запуск таймеров
            Console.WriteLine("Запуск:\n");
            for (int i = 0; i < timers.Length; i++)
            {
                //Подписка на события
                array[i].Init(timers[i]);
                try
                {
                    //Запуск таймера
                    array[i].Run(time[i]);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Timer overflow. Skipping Timer.");
                }
            }

            //Демонстрация подписки нескольких классов на один таймер
            Console.WriteLine("Демонстрация подписки нескольких классов на один таймер:");
            CountDownTimer timer = new CountDownTimer("ОБЩИЙ");
            ICountDownNotifier item1 = new MethodClass();
            ICountDownNotifier item2 = new LambdaClass();
            item1.Init(timer);
            item2.Init(timer);
            item1.Run(10);
        }
    }
}
