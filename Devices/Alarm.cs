namespace rpi.Devices
{
    public class Alarm
    {
        protected readonly Diode diode;
        protected readonly Buzzer buzzer;
        protected readonly Button button;
        protected bool enabled = false;

        public Alarm(Diode diode, Buzzer buzzer, Button button)
        {
            this.diode = diode;
            this.buzzer = buzzer;
            this.button = button;
        }

        public void Start()
        {
            diode.StartBlinking();
            buzzer.StartBuzzing();
            button.StartListening(Stop);
            WaitForStop();
        }

        public void Stop()
        {
            diode.StopBliking();
            buzzer.StopBuzzing();
            enabled = false;
        }

        protected virtual void WaitForStop()
        {
            enabled = true;

            do
            {
                Waiter.WaitMiliseconds(250);
            } while (enabled);
        }
    }
}
