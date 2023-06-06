using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class InputManager : Subsystem
    {
        //const char ADD_TICKET_BUTTON = '+';
        //const char DELETE_TICKET_BUTTON = '-';
        const char CLEAR_TICKET_BUTTON = '5';
        const char CURSOR_UP_BUTTON = '8';
        const char CURSOR_DOWN_BUTTON = '2';

        bool useKeyInput;
        public bool UseKeyInput
        {
            set { useKeyInput = value; }
        }

        static InputManager instance;
        public static InputManager Instance
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
        }

        public override void SlowUpdate()
        {
            
        }

        public override void FastUpdate()
        {
            if (!useKeyInput)
            {
                return;
            }

            char UserInput = Console.ReadKey().KeyChar;
            //if (UserInput == ADD_TICKET_BUTTON)
            //{
            //    Ticket NewTicket = new Ticket();
            //    NewTicket.TicketItemName = TEST_TICKET_NAME;
            //    TicketManager.Instance.AddTicket(NewTicket);
            //}

            if(TicketManager.Instance.CurrentTicketCount == 0)
            {
                return;
            }

            if (UserInput == CURSOR_UP_BUTTON)
            {
                MoveUpTicket();
            }
            if (UserInput == CURSOR_DOWN_BUTTON)
            {
                MoveDownTicket();
            }
            if (UserInput == CLEAR_TICKET_BUTTON)
            {
                ClearTicket();
            }
        }

        public void ClearTicket()
        {
            TicketManager.Instance.ClearTicket();
        }

        public void MoveUpTicket() 
        {
            TicketManager.Instance.MoveUpTicketIndex();
        }

        public void MoveDownTicket()
        {
            TicketManager.Instance.MoveDownTicketIndex();
        }

        public void OpenCloseMenu()
        {
            //TODO this
        }
    }
}
