using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
