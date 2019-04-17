using rpi.Devices;
using rpi.DHT11Sensor;
using rpi.LCD;
using System;

namespace rpi
{
    class Program
    {
        static Thermometer thermometer;
        static LcdScreen lcdScreen;
        static DHTData dhtData;
        static DHT dhtSensor;
        static Alarm alarm;

        static double thermometerTemperature;
        static int temperatureThreshold;

        static void Main(string[] args)
        {
            InitializeObjects();

            while (true)
            {
                ReadSensorsData();

                if (thermometerTemperature > temperatureThreshold)
                {
                    DisplayAlarmInfo();
                    alarm.Start();
                    WaitForTempDecrease();
                }
                else
                    DisplaySensorsData();

                Waiter.WaitMiliseconds(8000);
            }
        }

        static void InitializeObjects()
        {
            AppConfigData config = new AppConfigData();

            lcdScreen = new LcdScreen(config.LcdRsPinNumber, config.LcdEPinNumber,
                config.LcdData0PinNumber,
                config.LcdData1PinNumber,
                config.LcdData2PinNumber,
                config.LcdData3PinNumber,
                config.LcdData4PinNumber,
                config.LcdData5PinNumber,
                config.LcdData6PinNumber,
                config.LcdData7PinNumber
                );

            alarm = new Alarm(new Diode(config.DiodePinNumber), 
                new Buzzer(config.BuzzerPinNumber), 
                new Button(config.ButtonPinNumber));

            thermometer = new Thermometer(config.DevicesDirectoryPath, 
                config.ThermometerAddress, 
                config.ContentFileName);

            dhtSensor = new DHT(config.DHTSensorPinNumber);
            dhtData = new DHTData();
            temperatureThreshold = config.TemperatureThreshold;
        }

        static void ReadSensorsData()
        {
            try
            {
                thermometerTemperature = thermometer.ReadTemperature();
                dhtData = dhtSensor.GetData();
            }
            catch (Exception) { } //reading from DHT sensor randomly throws exceptions...
        }

        static void WaitForTempDecrease()
        {
            DisplayTempDecreaseWaitingInfo();

            do
            {
                ReadSensorsData();
                Waiter.WaitMiliseconds(500);

            } while (thermometerTemperature > temperatureThreshold);
        }

        static void DisplaySensorsData()
        {
            lcdScreen.Clear();
            lcdScreen.Write($"Hum:{dhtData.Humidity}%");
            lcdScreen.NewLine();
            lcdScreen.Write($"Temp: {thermometerTemperature}C");
        }

        static void DisplayAlarmInfo()
        {
            lcdScreen.Clear();
            lcdScreen.Write("ALARM");
            lcdScreen.NewLine();
            lcdScreen.Write($"Temp above {temperatureThreshold}");
        }

        static void DisplayTempDecreaseWaitingInfo()
        {
            lcdScreen.Clear();
            lcdScreen.Write("Waiting for temp");
            lcdScreen.NewLine();
            lcdScreen.Write("decrease...");
        }
    }
}
