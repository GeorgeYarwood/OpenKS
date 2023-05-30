using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    //Reads ticket from till's serial output
    //and converts to ticket
    internal class SerialReader : Subsystem
    {
        const string DEFAULT_PORT = "COM1";
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

        [STAThread]
        public override void Init()
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
            Console.WriteLine(Output);
        }

        public override void FastUpdate()
        {

        }
    }
}
