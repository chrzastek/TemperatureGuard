using System;
using System.Device.Gpio;

namespace rpi.LCD
{
    public class LcdWriter
    {
        protected readonly LcdPins lcdPins;

        public LcdWriter(LcdPins lcdPins)
        {
            this.lcdPins = lcdPins;
        }

        public virtual void WriteText(string text)
        {
            foreach (char c in text)
            {
                lcdPins.SetEHighData();
                WriteData((short)c);
                System.Threading.Thread.Sleep(1);
                lcdPins.SetELowData();
            }
        }

        protected virtual void WriteData(short value)
        {
            char[] charArray = Convert.ToString(value, 2).PadLeft(8, '0').ToCharArray();
            Array.Reverse(charArray);
            
            for (int i = 0; i < charArray.Length; i++)
            {
                PinValue pinValueToSet = charArray[i] == '1' ? PinValue.High : PinValue.Low;
                lcdPins.WriteToPin(i, pinValueToSet);
            }
        }

        public virtual void SendInstruction(short instruction)
        {
            lcdPins.SetEHighInstruction();
            WriteData(instruction);
            lcdPins.SetELowInstruction();
        }

        public virtual void Dispose() => lcdPins.Dispose();
    }
}
