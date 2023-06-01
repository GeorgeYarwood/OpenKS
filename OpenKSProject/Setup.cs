using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class Setup : Subsystem
    {
        //Runs automatically first time and can then be re-run at any time

        //Prompt strings
        const string WELCOME_MESSAGE = "Welcome to OpenKS. Press any key to begin setup.";
        const string ERROR_MESSAGE = "Invalid option! Please try again.";

        const string ROLE_PROMPT = "What is the role of this KS node?";
        const string ROLE_PROMPT_OPTIONS = "1. Host   2. Slave   3. Ticket Receiever   4. Host + Ticket Receiver (Standalone)";

        const string HOST_DISCOVERY_PROMPT = "How will this KS node connect to the host?";
        const string HOST_DISCOVERY_PROMPT_OPTIONS = "1. IP address   2. Auto discovery";

        const string IP_ADDRESS_PROMPT = "Enter IP address of host KS node:";

        const string COM_PORT_PROMPT = "What COM port is the reciept output connected to on this device?";
        const string COM_PORT_PROMPT_OPTIONS = "COM ";

        const string TICKET_BEEP_PROMPT = "Enable ticket beep?";
        const string TICKET_BEEP_PROMPT_OPTIONS = "(Y/N)";

        const string LAST_TICKET_TIME_PROMPT = "Late ticket time threshhold";
        const string LAST_TICKET_TIME_PROMPT_OPTIONS = "00:00";

        const string SETUP_MENU_PROMPT = "Setup menu now?";
        const string SETUP_MENU_PROMPT_OPTIONS = "(Y/N)";

        bool hasSetupBeenRun = false;
        public bool HasSetupBeenRun
        {
            get { return hasSetupBeenRun; }
        }

        static Setup instance;
        public static Setup Instance
        {
            get { return instance; }
        }

        public override void Init()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                return;
            }
            SetupPrompts();
        }

        void SetupPrompts()
        {
            SerialisedConfig Config = new();

            Console.WriteLine(WELCOME_MESSAGE);
            Console.ReadKey();
            Console.WriteLine(ROLE_PROMPT);
            Console.WriteLine(ROLE_PROMPT_OPTIONS);
            GetUserInput:
            try
            {
                int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());
                if(UserInput == 1)
                {
                    Config.KsMode = KS_MODE.HOST;
                }
                else if(UserInput == 2)
                {
                    Config.KsMode = KS_MODE.SLAVE;
                }
                else if (UserInput == 3)
                {
                    Config.KsMode = KS_MODE.TICKET_RECEIVER;
                }
                else if (UserInput == 4)
                {
                    Config.KsMode = KS_MODE.STANDALONE;
                }
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetUserInput;
            }

            Console.WriteLine(HOST_DISCOVERY_PROMPT);
            Console.ReadKey();


        }

    }
}
