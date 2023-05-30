using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class TicketManager : Subsystem
    {
        List<Ticket> currentTickets = new List<Ticket>();
        public List<Ticket> CurrentTickets
        {
            get { return currentTickets; }
        }

        public int CurrentTicketCount
        {
            get { return CurrentTickets.Count; }
        }

        int currentTicketIndex = 0;
        public int CurrentTicketIndex
        {
            get { return currentTicketIndex; }
        }

        static TicketManager instance;
        public static TicketManager Instance
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
            if(currentTicketIndex > CurrentTicketCount)
            {
                currentTicketIndex = CurrentTicketCount;
            }
            else if(currentTicketIndex < 0)
            {
                currentTicketIndex = 0;
            }
        }

        public override void SlowUpdate()
        {
            UpdateTicketTimes();
        }

        void UpdateTicketTimes()
        {
            for (int t = 0; t < currentTickets.Count; t++)
            {
                Ticket ThisTicket = currentTickets[t];
                ThisTicket.CurrentTicketTime
                    = GetCurrentUnixTime() - ThisTicket.TicketCreationTime;
            }
            UiManager.Instance.ReDrawTickets();
        }

        long GetCurrentUnixTime()
        {
            DateTime CurrentTime = DateTime.UtcNow;
            return ((DateTimeOffset)CurrentTime).ToUnixTimeSeconds();
        }

        public void AddTicket(Ticket NewTicket)
        {
            NewTicket.TicketCreationTime = GetCurrentUnixTime();
            currentTickets.Add(NewTicket);
            UiManager.Instance.ReDrawTickets();
        }

        public void DeleteTicket()
        {
            currentTickets.Remove(CurrentTickets[CurrentTicketIndex]);
            CheckSelectionIsInRange();
            UiManager.Instance.ReDrawTickets();
        }

        public void MoveDownTicketIndex()
        {
            //Down as in physically down the screen
            if (currentTicketIndex < CurrentTicketCount -1)
            {
                currentTicketIndex++;
            }
            UiManager.Instance.ReDrawTickets();
        }

        public void MoveUpTicketIndex()
        {
            if (currentTicketIndex > 0)
            {
                currentTicketIndex--;
            }
            UiManager.Instance.ReDrawTickets();
        }

        public void ClearTicket()
        {
            //TODO Add seperate clearning logic
            currentTickets.Remove(CurrentTickets[CurrentTicketIndex]);
            CheckSelectionIsInRange();
            UiManager.Instance.ReDrawTickets();
        }

        void CheckSelectionIsInRange()
        {
            if(currentTicketIndex >= CurrentTicketCount)
            {
                currentTicketIndex = CurrentTicketCount - 1;
            }
        }
    }
}
