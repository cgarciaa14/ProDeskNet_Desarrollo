using System;
using System.Collections.Generic;
////using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace buro05
{
    public class cssburo
    {
        private string[] IP;
        private string request;
        private int port;
        private string result;

        public string myresult
        {
            get { return result; }
            set { result = value; }
        }

        public int myport
        {
            get { return port; }
            set { port = value; }
        }

        public string cadena
        {
            get { return request; }
            set { request = value; }
        }

        public string[] args
        {
            get
            {
                return IP;
            }
            set
            {
                IP = value;
            }
        }



        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;

            // Get host related information.

            IPAddress hostIPAddress = IPAddress.Parse(server);

            IPEndPoint ipe = new IPEndPoint(hostIPAddress, port);
            Socket tempSocket =
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                s = tempSocket;
            }

            return s;
        }

        // This method requests the home page content for the specified server.
        public string SocketSendReceive(string server, int port)
        {
            //string request = "GET / HTTP/1.1\r\nHost: " + server +
            //    "\r\nConnection: Close\r\n\r\n";
            //string request = cadena.Replace("|", "\r\n");   //prueba remplaza enters !!!

            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);

            Byte[] newArray = new Byte[bytesSent.Length + 1];
            bytesSent.CopyTo(newArray, 0);
            newArray[bytesSent.Length] = 19;

            Byte[] bytesReceived = new Byte[2000];

            // Create a socket connection with the specified server and port.
            Socket s = ConnectSocket(server, port);
            s.LingerState = new LingerOption(true, 1);
            s.ReceiveTimeout = 60000;

            if (s == null)
                return ("Connection failed");

            // Send request to the server.
            s.Send(newArray, newArray.Length, 0);            

            // Receive the server home page content.
            int bytes = 0;
            string page = "";

            // The following will block until te page is transmitted.
            //Solo se toma la primera trama.

            //int valorMax = 0;
            //Boolean bloquing;
            //Boolean conected;
            //Boolean fragment;


            //while (valorMax != s.Available)
            //{
            //    valorMax = s.Available;
            //    bloquing = s.Blocking;
            //    conected = s.Connected;
            //    fragment = s.DontFragment;
            //}


            do
            {
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                if (s.LingerState.LingerTime == 0) {
                    page = "Error: Excedio el tiempo de respuesta de recepcion de datos";
                    return page;
                }
            }
            while (!page.Contains("02**"));
                                                       
            s.Close();

            return page;
        }

        public void Main(string[] args)
        {
            string host;
            //int port = 35001;

            if (args.Length == 0)
                // If no server name is passed as argument to this program, 
                // use the current host name as the default.
                host = Dns.GetHostName();
            else
                host = args[0];

            result = SocketSendReceive(host, port);
            //Console.WriteLine(result);
        }
    }
}
