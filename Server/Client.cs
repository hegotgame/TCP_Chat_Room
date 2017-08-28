using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        NetworkStream stream;
        TcpClient client;
        public string UserName;
        public Client(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            AskForUserName();
        }
        public void DisplayUserIsConnected()
        {
            if (UserName != null) { Console.WriteLine($"{UserName} has connected"); };
            Attribution();
        }
        private void Attribution()
        {
            Console.WriteLine(DateTime.Now + "   " + UserName + "\n");
        }

        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
        }
        public void Recieve()
        {
            while (true)
            {
                byte[] recievedMessage = new byte[256];
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                string recievedMessageString = Encoding.ASCII.GetString(recievedMessage).Trim('\0');
                Console.WriteLine(recievedMessageString);
                Attribution();
                Message newMessage = new Message(this, recievedMessageString);
                Server.BroadCast(newMessage);
            }
        }

        public string RecieveUserName()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
            recievedMessageString = recievedMessageString.Trim('\0');
            return recievedMessageString;
        }

        private void AskForUserName()
        {
            
            UserName = RecieveUserName();
        }
    }
}
