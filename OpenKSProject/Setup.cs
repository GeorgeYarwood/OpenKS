using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class Setup : Subsystem //Not treated as a subsystem but not changing because lazy
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
        const string COM_PORT_PROMPT_OPTIONS = "COM";

        const string COM_BAUD_RATE_PROMPT = "What baud rate will you be connecting at? (If unsure use 9600)";

        const string TICKET_BEEP_PROMPT = "Enable ticket beep?";
        const string TICKET_BEEP_PROMPT_OPTIONS = "(Y/N)";

        const string LAST_TICKET_TIME_PROMPT = "Late ticket time threshhold";
        const string LAST_TICKET_TIME_PROMPT_OPTIONS = "00:00";

        const string SETUP_MENU_PROMPT = "Setup menu now?";
        const string SETUP_MENU_PROMPT_OPTIONS = "(Y/N)";

        const string LOADING_OPEN_KS_MESSAGE = "Setup complete. Loading OpenKS...";

        const char NO_INPUT_KEY = 'n';
        const char YES_INPUT_KEY = 'y';
        const string NEW_LINE = "\n";
        const ConsoleKey ESCAPE_KEY = ConsoleKey.Escape;

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

        SerialisedConfig config = new();
        public SerialisedConfig Config
        {
            get { return config; }
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
            SetupPrompts();
        }

        void SetupPrompts()
        {
            Console.WriteLine(WELCOME_MESSAGE);
            Console.ReadKey();
            Console.WriteLine(ROLE_PROMPT);
            Console.WriteLine(ROLE_PROMPT_OPTIONS);
        GetUserRoleInput:
            try
            {
                int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());
                if (UserInput == 1)
                {
                    config.KsMode = KS_MODE.HOST;
                }
                else if (UserInput == 2)
                {
                    config.KsMode = KS_MODE.SLAVE;
                }
                else if (UserInput == 3)
                {
                    config.KsMode = KS_MODE.TICKET_RECEIVER;
                }
                else if (UserInput == 4)
                {
                    config.KsMode = KS_MODE.STANDALONE;
                }
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetUserRoleInput;
            }
            Console.WriteLine(NEW_LINE);

        GetHostDiscoveryInput:
            Console.WriteLine(HOST_DISCOVERY_PROMPT);
            Console.WriteLine(HOST_DISCOVERY_PROMPT_OPTIONS);
            try
            {
                int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());
                if (UserInput == 1)
                {
                    config.HostDiscoveryMode = HOST_DISCOVERY_MODE.IP_ADDRESS;
                }
                else if (UserInput == 2)
                {
                    config.HostDiscoveryMode = HOST_DISCOVERY_MODE.AUTO_DISCOVERY;
                }
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetHostDiscoveryInput;
            }

            Console.WriteLine(NEW_LINE);

        //GetIpAddressPrompt:
        //    Console.WriteLine(IP_ADDRESS_PROMPT);
        //    Console.WriteLine();
        //    try
        //    {
        //        int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());
        //        if (UserInput == 1)
        //        {
        //            Config.HostDiscoveryMode = HOST_DISCOVERY_MODE.IP_ADDRESS;
        //        }
        //        else if (UserInput == 2)
        //        {
        //            Config.HostDiscoveryMode = HOST_DISCOVERY_MODE.AUTO_DISCOVERY;
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine(ERROR_MESSAGE);
        //        goto GetIpAddressPrompt;
        //    }

        //Console.WriteLine(NEW_LINE);

        GetComPortInput:
            Console.WriteLine(COM_PORT_PROMPT);
            Console.WriteLine(COM_PORT_PROMPT_OPTIONS);
            try
            {
                string UserInput = Console.ReadKey().KeyChar.ToString();
                config.ComPort = COM_PORT_PROMPT_OPTIONS + UserInput;
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetComPortInput;
            }
            Console.WriteLine(NEW_LINE);

        GetComBaudRateInput:
            Console.WriteLine(COM_BAUD_RATE_PROMPT);
            try
            {
                int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());
                config.BaudRate = UserInput;
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetComBaudRateInput;
            }
            Console.WriteLine(NEW_LINE);


        GetTicketBeepInput:
            Console.WriteLine(TICKET_BEEP_PROMPT);
            Console.WriteLine(TICKET_BEEP_PROMPT_OPTIONS);
            try
            {
                char UserInput = Console.ReadKey().KeyChar;
                if (UserInput == YES_INPUT_KEY)
                {
                    config.TicketBeepEnabled = true;
                }
                else if (UserInput == NO_INPUT_KEY)
                {   
                    config.TicketBeepEnabled = false;
                }
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetTicketBeepInput;
            }

            Console.WriteLine(NEW_LINE);

        GetLateTicketTimeInput:
            Console.WriteLine(LAST_TICKET_TIME_PROMPT);
            Console.WriteLine(LAST_TICKET_TIME_PROMPT_OPTIONS);
            try
            {
                int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());

                //Config.LateTicketThreshhold = UserInput 
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetLateTicketTimeInput;
            }

            Console.WriteLine(NEW_LINE);

        GetSetupMenuInput:
            Console.WriteLine(SETUP_MENU_PROMPT);
            Console.WriteLine(SETUP_MENU_PROMPT_OPTIONS);
            try
            {
                char UserInput = Console.ReadKey().KeyChar;
                if (UserInput == YES_INPUT_KEY)
                {
                    SetupMenu();
                }
                else if (UserInput == NO_INPUT_KEY)
                {
                    LoadMainProgram();
                    return;
                }
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetSetupMenuInput;
            }

            Console.WriteLine(NEW_LINE);
        }

        void SetupMenu()
        {
            ConsoleKeyInfo UserInput;
            while (true)
            {
                UserInput = Console.ReadKey();
                if (UserInput.Key == ESCAPE_KEY)
                {
                    LoadMainProgram();
                    return;
                }
            }
        }

        void LoadMainProgram()
        {
            Console.WriteLine(NEW_LINE);
            hasSetupBeenRun = true; //Main just listens for this bool and will continue when it's true
            Console.WriteLine(LOADING_OPEN_KS_MESSAGE);
        }

        public SerialisedConfig ReturnKsConfig()
        {
            return Config;
        }
    }
}

