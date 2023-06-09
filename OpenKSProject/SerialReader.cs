﻿using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Data;

namespace OpenKSProject
{
    //Reads ticket from till's serial output
    //and converts to ticket
    internal class SerialReader : Subsystem
    {
        //Serial port connection settings
        const string DEFAULT_PORT = "COM3";
        const int DEFAULT_BAUD = 9600;

        string configuredPort = DEFAULT_PORT;
        public string ConfiguredPort
        {
            set { configuredPort = value; }
        }

        int configuredBaud = DEFAULT_BAUD;
        public int ConfiguredBaud
        {
            set { configuredBaud = value; }
        }

        SerialPort ticketInputPort;

        //SQL server settings

                                   
        SqlConnection? sqlClient;

        public override void Init()
        {
            SetupSerialPort();
        }

        [STAThread]
        void SetupSerialPort()
        {
            ticketInputPort = new SerialPort(configuredPort, configuredBaud, Parity.None, 8, StopBits.One);

            ticketInputPort.DataReceived
                += new SerialDataReceivedEventHandler(HandleDataRecieved);

            ticketInputPort.Open();
        }

        public override void SlowUpdate()
        {

        }

        void HandleDataRecieved(object Sender, SerialDataReceivedEventArgs _)
        {
            string Output = ticketInputPort.ReadExisting();
            Output = Output.ToLower();
            string PotentialMatch = string.Empty;
            string CurrentMenuItem = string.Empty; ;

            //Loop through each menu item in local DB(list)
            //check for names with spaces
            //For each character in reciept string 
            //Check from character + length of this menu item
            //If EXACT match
            //Break out/add ticket with name

            for (int mi = 0; mi < MenuSearch.runtimeMenu.Count; mi++)
            {
                CurrentMenuItem = MenuSearch.runtimeMenu[mi].ItemName;
                CurrentMenuItem = CurrentMenuItem.ToLower();
                for (int c = 0; c < Output.Length; c++)
                {
                    PotentialMatch = string.Empty;
                    int CharacterCount = 0;
                    while(CharacterCount < CurrentMenuItem.Length)
                    {
                        if(c + CharacterCount >= Output.Length)
                        {
                            break;
                        }
                        PotentialMatch += Output[c + CharacterCount];
                        CharacterCount++;
                    }
                    if (PotentialMatch == CurrentMenuItem)
                    {
                        Ticket NewTicket = new();
                        NewTicket.TicketItemName = CurrentMenuItem;
                        TicketManager.Instance.AddTicket(NewTicket);
                        //return;
                    }
                }
            }

            if (PotentialMatch == CurrentMenuItem)
            {
                Ticket NewTicket = new();
                NewTicket.TicketItemName = CurrentMenuItem;
                TicketManager.Instance.AddTicket(NewTicket);
                return;
            }
            //If we're here, no menu item was found in the database! (Throw a warning somehow)
        }

        public override void FastUpdate()
        {

        }
    }
}
