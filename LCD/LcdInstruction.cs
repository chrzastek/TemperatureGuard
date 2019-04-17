namespace rpi.LCD
{
    public enum LcdInstruction
    {
        Clear = 1,
        CursorHome = 2,
        DisplayOn = 15,
        ShiftDisplayLeft = 24,
        ShiftDisplayRight = 28,
        FunctionSet00111100 = 60,
        NewLine = 192
    }
}
