using System.Device.Gpio;
using System.Threading.Tasks;

namespace rpi.Devices
{
    public abstract class Device
    {
        protected const int frequency = 500;
        protected int pinNumber { get; set; }
        protected bool act;

        protected TaskFactory taskFactory = new TaskFactory();
        protected GpioController gpioController = new GpioController(PinNumberingScheme.Board);

        public Device(int pinNumber)
        {
            this.pinNumber = pinNumber;
            gpioController.OpenPin(pinNumber);
            gpioController.SetPinMode(pinNumber, PinMode.Output);
        }

        protected virtual void StartActing()
        {
            act = true;
            taskFactory.StartNew(x =>
            {
                while (act)
                {
                    PowerUp();
                    Wait(frequency);
                    PowerDown();
                    Wait(frequency);
                }
            }, null);
        }

        protected virtual void StopActing()
        {
            act = false;
            PowerDown();
        }

        protected void Wait(int timeInMiliseconds) => System.Threading.Thread.Sleep(timeInMiliseconds);

        protected void PowerUp() => gpioController.Write(pinNumber, PinValue.High);

        protected void PowerDown() => gpioController.Write(pinNumber, PinValue.Low);
    }
}
