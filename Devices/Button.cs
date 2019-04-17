using System;
using System.Device.Gpio;
using System.Threading;

namespace rpi.Devices
{
    public class Button : Device
    {
        public Button(int pinNumber) : base(pinNumber)
        {
            gpioController.SetPinMode(pinNumber, PinMode.InputPullUp);
        }

        protected virtual bool WasPressed()
        {
            PinValue buttonPinVale = gpioController.Read(pinNumber);
            return buttonPinVale == PinValue.Low;
        }

        public void StartListening(Action onPressed)
        {
            taskFactory.StartNew(x =>
            {
                do
                {
                    if(WasPressed())
                        onPressed.Invoke();

                    Thread.Sleep(300);

                } while (true);
            }, null);
        }
    }
}
