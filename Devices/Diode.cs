namespace rpi.Devices
{
    public class Diode : Device
    {
        public Diode(int pinNumber) : base(pinNumber) { }

        public void StartBlinking() => StartActing();

        public void StopBliking() => StopActing();

        public void LightUp() => PowerUp();

        public void LightDown() => PowerDown();
    }
}
