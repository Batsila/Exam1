using CommandsHandler.Service;
using System;
using System.ServiceModel;

namespace CommandsHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sh = new ServiceHost(typeof(CommandsReceiverService)))
            {
                sh.Open();

                Console.WriteLine("Service STARTED");
                Console.ReadLine();
            }
        }
    }
}
