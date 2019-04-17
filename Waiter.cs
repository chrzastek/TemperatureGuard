using System;
using System.Threading;
using System.Threading.Tasks;

namespace rpi
{
    public static class Waiter
    {
        public static void WaitMiliseconds(int miliseconds) => Thread.Sleep(miliseconds);

        public static void WaitTimeSpan(TimeSpan duration) => Task.Delay(duration);
    }
}
