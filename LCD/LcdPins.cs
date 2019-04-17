using System.Device.Gpio;

namespace rpi.LCD
{
    public class LcdPins
    {
        protected readonly int[] dataPins;

        protected readonly int rsPinNumber;
        protected readonly int ePinNumber;

        protected readonly GpioController gpio;

        public LcdPins(int rsPinNumber, int ePinNumber, int d0PinNumber, int d1PinNumber, int d2PinNumber,
            int d3PinNumber, int d4PinNumber, int d5PinNumber, int d6PinNumber, int d7PinNumber)
        {
            this.rsPinNumber = rsPinNumber;
            this.ePinNumber = ePinNumber;

            dataPins = new int[8]
            {
                d0PinNumber, d1PinNumber, d2PinNumber, d3PinNumber, d4PinNumber, d5PinNumber, d6PinNumber, d7PinNumber
            };

            gpio = new GpioController(PinNumberingScheme.Board);
            InitializeAllPins();
        }

        public int this[int i]
        {
            get { return dataPins[i]; }
        }

        protected virtual void InitializeAllPins()
        {
            InitializePin(rsPinNumber);
            InitializePin(ePinNumber);
            System.Array.ForEach(dataPins, p => InitializePin(p));
        }

        protected virtual void InitializePin(int pinNumber)
        {
            gpio.OpenPin(pinNumber);
            gpio.SetPinMode(pinNumber, PinMode.Output);
            gpio.Write(pinNumber, PinValue.Low);
        }

        public virtual void WriteToPin(int pinIndex, PinValue pinValue) 
            => gpio.Write(this[pinIndex], pinValue);

        public virtual void SetEHighInstruction()
        {
            gpio.Write(ePinNumber, PinValue.High);
            gpio.Write(rsPinNumber, PinValue.Low);
        }

        public virtual void SetELowInstruction()
        {
            gpio.Write(ePinNumber, PinValue.Low);
            gpio.Write(rsPinNumber, PinValue.Low);
        }

        public virtual void SetEHighData()
        {
            gpio.Write(ePinNumber, PinValue.High);
            gpio.Write(rsPinNumber, PinValue.High);
        }

        public virtual void SetELowData()
        {
            gpio.Write(ePinNumber, PinValue.Low);
            gpio.Write(rsPinNumber, PinValue.High);
        }

        public virtual void Dispose()
        {
            gpio.Write(rsPinNumber, PinValue.Low);
            gpio.ClosePin(rsPinNumber);

            gpio.Write(ePinNumber, PinValue.Low);
            gpio.ClosePin(ePinNumber);

            foreach (int pinNumber in dataPins)
            {
                gpio.Write(pinNumber, PinValue.Low);
                gpio.ClosePin(pinNumber);
            }
        }
    }
}
