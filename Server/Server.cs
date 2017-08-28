using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static Client client;
        static Queue<string> MessageLog;
        TcpListener server;
        static Dictionary<string, Client> CurrentClients;
        static Dictionary<string, Thread> RecieveThread;

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("192.168.0.119"), 9999);
            server.Start();
            MessageLog = new Queue<string>();
            CurrentClients = new Dictionary<string, Client>();
            RecieveThread = new Dictionary<string, Thread>();

        }
        public void Run()
        {
            Task.Run(()=>AcceptClient());
           
        }
        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                //server tells everyone that client connected
                //utilizing an observer pattern
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                AddNewClient(client);

            }
        }

        private void AddNewClient(Client NewClient)
        {
            CurrentClients.Add(NewClient.UserName, NewClient);

            RecieveThread.Add(NewClient.UserName, new Thread(new ThreadStart(NewClient.Recieve)));

            RecieveThread[NewClient.UserName].Start();
        }

        public static void BroadCast(Message message)
        {
            MessageLog.Enqueue(Convert.ToString(message.Body));
            foreach(KeyValuePair<string, Client> User in CurrentClients)
            {
                if (User.Key == message.UserId) continue;
                User.Value.Send(MessageLog.Dequeue());
            }
        }
    }
}
