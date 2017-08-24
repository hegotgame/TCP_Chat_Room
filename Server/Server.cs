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
        Dictionary<string, TcpClient> UserId;
        string ClientMessage;
        
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("192.168.0.104"), 9999);
            server.Start();
            MessageLog = new Queue<string>();
        }
        public void Run()
        {
            AcceptClient();

            while (true)
            {
                MessageLog.Enqueue(client.Recieve());
                ClientMessage = MessageLog.Dequeue();
                Respond(ClientMessage);
            }
 
        }
        private void AcceptClient()
        {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}
