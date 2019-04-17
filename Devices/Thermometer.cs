using System;
using System.IO;

namespace rpi.Devices
{
    public class Thermometer
    {
        protected readonly string devicesDirectoryPath = "/sys/bus/w1/devices";
        protected readonly string thermometerAddress = "28-01186ea73fff";
        protected readonly string contentFileName = "w1_slave";

        public Thermometer(string devicesDirectoryPath, string thermometerAddress, string contentFileName)
        {
            this.devicesDirectoryPath = devicesDirectoryPath;
            this.thermometerAddress = thermometerAddress;
            this.contentFileName = contentFileName;
        }

        public virtual double ReadTemperature()
        {
            double temperature = double.MaxValue;
            string contentFilePath = Path.Combine(devicesDirectoryPath, thermometerAddress, contentFileName);

            if(File.Exists(contentFilePath))
            {
                string fileContent = File.OpenText(contentFilePath).ReadToEnd();
                string temperaturePiece = fileContent.Split(new string[] { "t=" }, StringSplitOptions.RemoveEmptyEntries)[1];
                temperature = double.Parse(temperaturePiece) / 1000;
            }

            return temperature;
        }
    }
}
