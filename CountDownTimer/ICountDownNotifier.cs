using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LabWork_Delegates_Timer
{
    public interface ICountDownNotifier
    {
        public void Init(CountDownTimer timer);
        public void Run(int time);
    }
}
