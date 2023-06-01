using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public enum KS_MODE { HOST, SLAVE, TICKET_RECEIVER, STANDALONE}
public enum HOST_DISCOVERY_MODE { IP_ADDRESS, AUTO_DISCOVERY}

namespace OpenKSProject
{
    internal class SerialisedConfig
    {
        KS_MODE ksMode;
        public KS_MODE KsMode
        {
            get { return ksMode; }
            set { ksMode = value; }
        }

        HOST_DISCOVERY_MODE hostDiscoveryMode;
        public HOST_DISCOVERY_MODE HostDiscoveryMode
        {
            get { return hostDiscoveryMode; }
            set { hostDiscoveryMode = value; }
        }

        IPAddress? hostIPAddress;
        public IPAddress? HostIPAddress
        {
            get { return hostIPAddress; }
            set { hostIPAddress = value; }
        }

        int? comPort;
        public int? ComPort
        {
            get { return comPort; }
            set { comPort = value; }
        }

        bool ticketBeepEnabled;
        public bool TicketBeepEnabled
        {
            get { return ticketBeepEnabled; }
            set { ticketBeepEnabled = value; }
        }

        TimeOnly lateTicketThreshhold;
        public TimeOnly LateTicketThreshhold
        {
            get { return lateTicketThreshhold;}
            set { lateTicketThreshhold = value; }
        }
    }
}
