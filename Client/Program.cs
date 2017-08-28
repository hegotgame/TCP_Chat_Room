using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your username:");
            Client client = new Client("192.168.88.142", 8080);
            Thread SendMessages = new Thread(new ThreadStart(client.Send));
            Thread Recievemessages = new Thread(new ThreadStart(client.Recieve));

            SendMessages.Start();
            Recievemessages.Start();
        }
    }
}
