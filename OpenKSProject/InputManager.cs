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

        public override void Init()
        {
            
        }

        public override void SlowUpdate()
        {
            
        }

        public override void FastUpdate()
        {
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
                TicketManager.Instance.MoveUpTicketIndex();
            }
            if (UserInput == CURSOR_DOWN_BUTTON)
            {
                TicketManager.Instance.MoveDownTicketIndex();
            }
            if (UserInput == CLEAR_TICKET_BUTTON)
            {
                TicketManager.Instance.ClearTicket();
            }
        }
    }
}
