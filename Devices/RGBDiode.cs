using System.Device.Gpio;

namespace rpi.Devices
{
    public class RGBDiode : Device
    {
        protected readonly int greenPinNumber;
        protected readonly int bluePinNumber;

        public RGBDiode(int redPinNumber, int greenPinNumber, int bluePinNumber) : base(redPinNumber)
        {
            this.greenPinNumber = greenPinNumber;
            this.bluePinNumber = bluePinNumber;
            ConfigurePins();
        }

        protected virtual void ConfigurePins()
        {
            gpioController.OpenPin(greenPinNumber);
            gpioController.SetPinMode(greenPinNumber, PinMode.Output);

            gpioController.OpenPin(bluePinNumber);
            gpioController.SetPinMode(bluePinNumber, PinMode.Output);
        }

        public void StartBlinking() => StartActing();

        public void StopBlinking() => StopActing();

        protected override void StartActing()
        {
            taskFactory.StartNew(x =>
            {
                while (act)
                {
                    LightOnRed();
                    Waiter.WaitMiliseconds(frequency);
                    LightOffRed();

                    LightOnGreen();
                    Waiter.WaitMiliseconds(frequency);
                    LightOffGreen();

                    LightOnBlue();
                    Waiter.WaitMiliseconds(frequency);
                    LightOffBlue();
                }
            }, null);
        }

        protected override void StopActing()
        {
            act = false;
            LightOffRed();
            LightOffGreen();
            LightOffBlue();
        }

        protected virtual void LightOnRed() => gpioController.Write(pinNumber, PinValue.High);
        protected virtual void LightOffRed() => gpioController.Write(pinNumber, PinValue.Low);

        protected virtual void LightOnGreen() => gpioController.Write(greenPinNumber, PinValue.High);
        protected virtual void LightOffGreen() => gpioController.Write(greenPinNumber, PinValue.Low);

        protected virtual void LightOnBlue() => gpioController.Write(bluePinNumber, PinValue.High);
        protected virtual void LightOffBlue() => gpioController.Write(bluePinNumber, PinValue.Low);

    }
}
