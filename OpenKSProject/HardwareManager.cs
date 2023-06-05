using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class HardwareManager : Subsystem
    {
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
        }
        public override void FastUpdate()
        {

        }

        public override void SlowUpdate()
        {

        }

        public void TicketBeep()
        {
            if (!ticketBeepEnabled)
            {
                return;
            }
        }
    }
}
