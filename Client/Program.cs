using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Client client = new Client("10.123.95.34", 9999);
            while (true)
            {
                client.Send();
                client.Recieve();
                Console.ReadLine();
            }
        }
    }
}
