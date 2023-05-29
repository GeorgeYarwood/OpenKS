using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class Ticket
    {
        string ticketItemName = string.Empty;
        public string TicketItemName
        {
            get { return ticketItemName; }
            set { ticketItemName = value; }
        }

        int ticketTableNumber;
        public int TicketTableNumber
        {
            get { return ticketTableNumber; }
            set { ticketTableNumber = value; }
        }

        long ticketCreationTime;
        public long TicketCreationTime
        {
            get { return ticketCreationTime; }
            set { ticketCreationTime = value; }
        }

        long currentTicketTime;
        public long CurrentTicketTime
        {
            get { return currentTicketTime; }
            set { currentTicketTime = value; }
        }
    }
}
