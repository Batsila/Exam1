using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommandsHandler
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CommandsReceiverService" in both code and config file together.
    public class CommandsReceiverService : IReceiver
    {
        public bool SendCommands(params MouseCommand[] command)
        {
            Task.Factory.StartNew(() =>
            {
                var sb = new StringBuilder();
                sb.Append(DateTime.Now);

                for (int i = 0; i < command.Length; i++)
                {
                    if (i > 0)
                        sb.Append("+");
                    var item = command[i];

                    if (item is MouseMoveCommand moveCmd)
                        sb.AppendFormat("{0}-{1}", moveCmd.CommandName, moveCmd.Quantity);
                    else
                        sb.AppendFormat("{0}", item.CommandName);
                }

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine(sb.ToString());
            });

            return true;
        }
    }
}
