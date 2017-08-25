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

            server = new TcpListener(IPAddress.Parse("192.168.0.178"), 9999);
            server.Start();
            MessageLog = new Queue<string>();
            UserId = new Dictionary<string, TcpClient>();

        }
        public void Run()
        {
<<<<<<< HEAD
            while (true)
            {
                AcceptClient();

            }
=======
                Parallel.Invoke(AcceptClient, BroadCast);
                
>>>>>>> cbd25fe58c98fa11b0ee06f42f8b5af013ebb90a
        }
        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
<<<<<<< HEAD
                MessageLog.Enqueue(client.Recieve());
                ClientMessage = MessageLog.Dequeue();
                Respond(ClientMessage);
            // Broadcast to all new user has joined
            // Add to dictionary
            //                NewUserID();
            //               client.UserId = 
=======
                Thread newClient = new Thread(new ThreadStart(RunClient(client)));
            }

>>>>>>> cbd25fe58c98fa11b0ee06f42f8b5af013ebb90a
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
