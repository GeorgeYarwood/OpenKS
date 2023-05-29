using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class UiManager : Subsystem
    {
        const string CURSOR_AREA_LEFT = "(";
        const string CURSOR_AREA_RIGHT = ")";
        const string CURSOR = "*";
        const string BLANK_SPACE = " ";
        const string NEW_LINE = "\n";

        static UiManager instance;
        public static UiManager Instance
        {
            get { return instance; }
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

        public void ReDrawTickets()
        {
            Console.Clear();
            int Iterator = TicketManager.Instance.CurrentTicketCount;
            for (int t = 0; t < Iterator; t++)
            {
                Ticket ThisTicket = TicketManager.Instance.CurrentTickets[t];
                if (ThisTicket != null)
                {
                    string TicketContents = CURSOR_AREA_LEFT;
                    if(t == TicketManager.Instance.CurrentTicketIndex)
                    {
                        TicketContents += CURSOR;
                    }
                    else
                    {
                        TicketContents += BLANK_SPACE;
                    }
                    TicketContents += CURSOR_AREA_RIGHT + BLANK_SPACE + ThisTicket.TicketItemName
                        + BLANK_SPACE + ConvertFromUnixTime(ThisTicket.CurrentTicketTime);
                    Console.WriteLine(TicketContents);
                }
            }
        }

        string ConvertFromUnixTime(long UnixTime)
        {
            DateTimeOffset Converted = DateTimeOffset.FromUnixTimeSeconds(UnixTime);
            return Converted.Minute.ToString() + Converted.Second.ToString();
        }
    }
}
