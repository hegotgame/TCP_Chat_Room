﻿using System;
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
            Console.WriteLine("Enter a username: ");
            UserName = Console.ReadLine();
            
        }

        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
        }
        public string Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);

            Console.WriteLine(recievedMessageString);

            return recievedMessageString;
        }


    }
}
