using System.Device.Gpio;
//using System.Net.NetworkInformation;
//using System.Runtime.InteropServices;

namespace OpenKSProject
{
    internal class HardwareManager : Subsystem
    {
        //TODO make redefinable in setup
        //Pin header numbers
        const int SPEAKER_PIN = 20;
        const int MENU_BUTTON_PIN = 21;
        const int CLEAR_BUTTON_PIN = 22;
        const int UP_BUTTON_PIN = 23;
        const int DOWN_BUTTON_PIN = 24;

        const float SPEAKER_TONE_LENGTH = 20.0f;

        GpioController gpioController;

        static HardwareManager instance;
        public static HardwareManager Instance
        {
            get { return instance; }
        }

        bool ticketBeepEnabled;
        public bool TicketBeepEnabled
        {
            get { return ticketBeepEnabled; }
            set { ticketBeepEnabled = value; }
        }

        public override void Init()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                return;
            }

            gpioController = new GpioController();
            OpenBumpPins();
        }

        public override void FastUpdate()
        {

        }

        public override void SlowUpdate()
        {

        }

        void OpenBumpPins()
        {
            gpioController.OpenPin(MENU_BUTTON_PIN, PinMode.InputPullUp);
            gpioController.RegisterCallbackForPinValueChangedEvent(
                MENU_BUTTON_PIN, PinEventTypes.Falling |
                PinEventTypes.Rising, HandleMenuButtonInput);

            gpioController.OpenPin(CLEAR_BUTTON_PIN, PinMode.InputPullUp);
            gpioController.RegisterCallbackForPinValueChangedEvent(
                CLEAR_BUTTON_PIN, PinEventTypes.Falling | 
                PinEventTypes.Rising, HandleClearButtonInput);

            gpioController.OpenPin(UP_BUTTON_PIN, PinMode.InputPullUp);
            gpioController.RegisterCallbackForPinValueChangedEvent(
                UP_BUTTON_PIN, PinEventTypes.Falling |
                PinEventTypes.Rising, HandleUpButtonInput);

            gpioController.OpenPin(DOWN_BUTTON_PIN, PinMode.InputPullUp);
            gpioController.RegisterCallbackForPinValueChangedEvent(
                DOWN_BUTTON_PIN, PinEventTypes.Falling |
                PinEventTypes.Rising, HandleDownButtonInput);
        }

        void HandleClearButtonInput(object Sender, EventArgs _)
        {
            InputManager.Instance.ClearTicket();
        }

        void HandleUpButtonInput(object Sender, EventArgs _)
        {
            InputManager.Instance.MoveUpTicket();
        }

        void HandleDownButtonInput(object Sender, EventArgs _)
        {
            InputManager.Instance.MoveDownTicket();
        }

        void HandleMenuButtonInput(object Sender, EventArgs _)
        {
            InputManager.Instance.OpenCloseMenu();
        }

        public void TicketBeep()
        {
            if (!ticketBeepEnabled)
            {
                return;
            }
            
            gpioController.OpenPin(SPEAKER_PIN, PinMode.Output);

            float LazyTimer = SPEAKER_TONE_LENGTH;

            while (LazyTimer > 0)
            {
                LazyTimer--;
            }

            gpioController.ClosePin(SPEAKER_PIN);
        }
    }
}
