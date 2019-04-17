using Microsoft.Extensions.Configuration;
using System;

namespace rpi
{
    public class AppConfigData
    {
        public int TemperatureThreshold  { get; protected set; }
        public int DHTSensorPinNumber { get; protected set; }

        public int LcdRsPinNumber { get; protected set; }
        public int LcdEPinNumber { get; protected set; }
        public int LcdData0PinNumber { get; protected set; }
        public int LcdData1PinNumber { get; protected set; }
        public int LcdData2PinNumber { get; protected set; }
        public int LcdData3PinNumber { get; protected set; }
        public int LcdData4PinNumber { get; protected set; }
        public int LcdData5PinNumber { get; protected set; }
        public int LcdData6PinNumber { get; protected set; }
        public int LcdData7PinNumber { get; protected set; }

        public int DiodePinNumber { get; protected set; }
        public int BuzzerPinNumber { get; protected set; }
        public int ButtonPinNumber { get; protected set; }

        public string DevicesDirectoryPath { get; protected set; }
        public string ThermometerAddress { get; protected set; }
        public string ContentFileName { get; protected set; }

        protected readonly IConfigurationRoot configuration;

        public AppConfigData()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            configuration = builder.Build();
            ReadConfig();
        }

        protected virtual void ReadConfig()
        {
            try
            {
                TemperatureThreshold = Convert.ToInt32(configuration["TemperatureThreshold"]);
                DHTSensorPinNumber = Convert.ToInt32(configuration["DHTSensorPinNumber"]);

                LcdRsPinNumber = Convert.ToInt32(configuration["LcdRsPinNumber"]);
                LcdEPinNumber = Convert.ToInt32(configuration["LcdEPinNumber"]);
                LcdData0PinNumber = Convert.ToInt32(configuration["LcdData0PinNumber"]);
                LcdData1PinNumber = Convert.ToInt32(configuration["LcdData1PinNumber"]);
                LcdData2PinNumber = Convert.ToInt32(configuration["LcdData2PinNumber"]);
                LcdData3PinNumber = Convert.ToInt32(configuration["LcdData3PinNumber"]);
                LcdData4PinNumber = Convert.ToInt32(configuration["LcdData4PinNumber"]);
                LcdData5PinNumber = Convert.ToInt32(configuration["LcdData5PinNumber"]);
                LcdData6PinNumber = Convert.ToInt32(configuration["LcdData6PinNumber"]);
                LcdData7PinNumber = Convert.ToInt32(configuration["LcdData7PinNumber"]);

                DiodePinNumber = Convert.ToInt32(configuration["DiodePinNumber"]);
                BuzzerPinNumber = Convert.ToInt32(configuration["BuzzerPinNumber"]);
                ButtonPinNumber = Convert.ToInt32(configuration["ButtonPinNumber"]);

                DevicesDirectoryPath = configuration["DevicesDirectoryPath"].ToString();
                ThermometerAddress = configuration["ThermometerAddress"].ToString();
                ContentFileName = configuration["ContentFileName"].ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error during reading application config file: {exception.Message}");
                throw exception;
            }
        }
    }
}
