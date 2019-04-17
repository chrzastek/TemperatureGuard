using System;
using System.Device.Gpio;
using System.Threading;

namespace rpi.DHT11Sensor
{
    public class DHT
    {
        protected int pinNumber;

        protected uint[] rawData;
        protected DateTime previousReading;
        protected GpioController gpioController;
        protected bool firstReading = true;

        public DHT(int pinNumber)
        {
            this.pinNumber = pinNumber;

            previousReading = DateTime.MinValue;
            rawData = new uint[6];

            InitializeGPIO();
        }

        protected virtual void InitializeGPIO()
        {
            gpioController = new GpioController(PinNumberingScheme.Board);
            gpioController.OpenPin(pinNumber);
            gpioController.SetPinMode(pinNumber, PinMode.Output);
            gpioController.Write(pinNumber, PinValue.High);
        }

        public DHTData GetData()
        {
            if (IsReadingLegal() && TryReadData())
            {
                return new DHTData()
                {
                    TempCelcius = rawData[2],
                    Humidity = rawData[0],
                    HeatIndex = ComputeHeatIndex(rawData[2], rawData[0])
                };
            }
            else
                throw new Exception();
        }

        protected virtual double ComputeHeatIndex(float temperatureCelsius, float percentHumidity)
        {
            // Heat index calculation. Different patterns for Fahrenheit and Kelvin!
            return -8.784695 +
                        1.61139411 * temperatureCelsius +
                        2.338549 * percentHumidity +
                    -0.14611605 * temperatureCelsius * percentHumidity +
                    -0.01230809 * Math.Pow(temperatureCelsius, 2) +
                    -0.01642482 * Math.Pow(percentHumidity, 2) +
                        0.00221173 * Math.Pow(temperatureCelsius, 2) * percentHumidity +
                        0.00072546 * temperatureCelsius * Math.Pow(percentHumidity, 2) +
                    -0.00000358 * Math.Pow(temperatureCelsius, 2) * Math.Pow(percentHumidity, 2);
        }

        protected virtual bool IsReadingLegal() =>
             !(((DateTime.UtcNow - previousReading).TotalMilliseconds < 2000));

        protected virtual void TickleSensor()
        {
            gpioController.SetPinMode(pinNumber, PinMode.Output);
            gpioController.Write(pinNumber, PinValue.High);
            Thread.Sleep(250);
            gpioController.Write(pinNumber, PinValue.Low);
            Thread.Sleep(20);
            gpioController.Write(pinNumber, PinValue.High);
            WaitMicroseconds(40);
            gpioController.SetPinMode(pinNumber, PinMode.Input);
            WaitMicroseconds(10);
        }

        protected bool TryReadData()
        {
            var now = DateTime.UtcNow;

            if (!firstReading && ((now - previousReading).TotalMilliseconds < 2000))
                return false;

            firstReading = false;
            previousReading = now;

            rawData[0] = rawData[1] = rawData[2] = rawData[3] = rawData[4] = 0;

            TickleSensor();

            if (ExpectPulse(PinValue.Low) == 0)
                return false;

            if (ExpectPulse(PinValue.High) == 0)
                return false;

            for (int i = 0; i < 40; ++i)
            {
                uint lowCycles = ExpectPulse(PinValue.Low);
                if (lowCycles == 0)
                    return false;

                uint highCycles = ExpectPulse(PinValue.High);
                if (highCycles == 0)
                    return false;

                rawData[i / 8] <<= 1;
                if (highCycles > lowCycles)
                    rawData[i / 8] |= 1;
            }

            return (rawData[4] == ((rawData[0] + rawData[1] + rawData[2] + rawData[3]) & 0xFF));
        }

        private uint ExpectPulse(PinValue pinValue)
        {
            uint count = 0;

            while (gpioController.Read(pinNumber) == (pinValue == PinValue.High))
            {
                count++;
                if (count == 10000)
                    return 0;
            }
            return count;
        }

        private void WaitMicroseconds(int microseconds)
        {
            long until = DateTime.UtcNow.Ticks + (microseconds * 10);
            while (DateTime.UtcNow.Ticks < until) { }
        }
    }
}