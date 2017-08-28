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
        Queue<string> MessageLog;
        TcpListener server;
        Dictionary<string, TcpClient> ClientsUsed;
        string ClientMessage;
        

        public Server()
        {

            server = new TcpListener(IPAddress.Parse("192.168.0.130"), 9999);
            server.Start();
            MessageLog = new Queue<string>();
            ClientsUsed = new Dictionary<string, TcpClient>();

        }
        public void Run()
        {
                Parallel.Invoke(AcceptClient, BroadCast);
        }
        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                //server tells everyone that client connected
                //utilizing an observer pattern
                Console.WriteLine("Connected a new user");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);

                ClientsUsed.Add(client.UserName, clientSocket);
                Task.Run(()=>RunClient(client));

            }
        }
        private void Respond(string body)
        {
             client.Send(body);
        }

        private ThreadStart RunClient(Client client)
        {
            while (true)
            {
                ClientMessage = client.Recieve();
                MessageLog.Enqueue(ClientMessage);
                Respond(ClientMessage);
            }
            
        }

        private void BroadCast()
        {
            //requires a foreach loop to send the latest message to every client
        }
    }
}
