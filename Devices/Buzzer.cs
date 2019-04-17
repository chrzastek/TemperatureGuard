namespace rpi.Devices
{
    public class Buzzer : Device
    {
        public Buzzer(int pinNumber) : base(pinNumber) { }

        public void StartBuzzing() => StartActing();

        public void StopBuzzing() => StopActing();

        protected void Buzz() => PowerUp();

        protected void Mute() => PowerDown();
    }
}
