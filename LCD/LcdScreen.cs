using System;

namespace rpi.LCD
{
    public class LcdScreen
    {
        protected readonly int maxLineLength = 17;
        protected readonly LcdWriter lcdWriter;

        public LcdScreen(int rsPinNumber, int ePinNumber, int d0PinNumber, int d1PinNumber, int d2PinNumber,
            int d3PinNumber, int d4PinNumber, int d5PinNumber, int d6PinNumber, int d7PinNumber)
        {
            lcdWriter = new LcdWriter(
                new LcdPins
                (
                rsPinNumber, ePinNumber, d0PinNumber,
                d1PinNumber, d2PinNumber, d3PinNumber,
                d4PinNumber, d5PinNumber, d6PinNumber, d7PinNumber
                ));

            Initialize();
        }

        protected virtual void Initialize()
        {
            lcdWriter.SendInstruction((short)LcdInstruction.FunctionSet00111100);
            Waiter.WaitTimeSpan(TimeSpan.FromMilliseconds(0.04));

            lcdWriter.SendInstruction((short)LcdInstruction.DisplayOn);
            Waiter.WaitTimeSpan(TimeSpan.FromMilliseconds(0.04));
        }

        public void Clear()
        {
            lcdWriter.SendInstruction((short)LcdInstruction.Clear);
            Waiter.WaitMiliseconds(10);
        }

        public void CursorHome()
        {
            lcdWriter.SendInstruction((short)LcdInstruction.CursorHome);
            Waiter.WaitTimeSpan(TimeSpan.FromMilliseconds(1.64));
        }

        public void Write(string text)
        {
            if(text.Length > maxLineLength)
            {
                string firstPart = text.Substring(0, maxLineLength);
                string secondPart = text.Substring(maxLineLength);

                lcdWriter.WriteText(firstPart);
                NewLine();
                lcdWriter.WriteText(secondPart);
            }
            else
                lcdWriter.WriteText(text);

            Waiter.WaitMiliseconds(10);
        }

        public void NewLine()
        {
            lcdWriter.SendInstruction((short)LcdInstruction.NewLine);
            Waiter.WaitMiliseconds(10);
        }

        public void ShiftDisplayLeft()
        {
            lcdWriter.SendInstruction((short)LcdInstruction.ShiftDisplayLeft);
            Waiter.WaitTimeSpan(TimeSpan.FromMilliseconds(0.04));
        }

        public void ShiftDisplayRight()
        {
            lcdWriter.SendInstruction((short)LcdInstruction.ShiftDisplayRight);
            Waiter.WaitTimeSpan(TimeSpan.FromMilliseconds(0.04));
        }

        public void Dispose() => lcdWriter.Dispose();
    }
}