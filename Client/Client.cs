using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        public string UserName;

        public Client(string IP, int port)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
            GetUserName();
            ConnectUserNameMessage();
        }
        private void GetUserName()
        {
            Console.WriteLine("Please enter your username:");
            UserName = Console.ReadLine();
        }
        public void ConnectUserNameMessage()
        {
            Console.WriteLine(UserName + " is connected. @ " + DateTime.Now);
        }
        public void Send()
        {
            string messageString = UI.GetInput();
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
            Attribution();
        }
        private void Attribution()
        {
            Console.WriteLine("from: " + UserName + " @ " + DateTime.Now);
        }
        public void Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            UI.DisplayMessage(Encoding.ASCII.GetString(recievedMessage));
        }
    }
}
