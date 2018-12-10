using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandsHandler
{
    public class CommandsReceiverService : IReceiver
    {
        private object _lock = new object();
        private bool _isFrame = false;

        List< KeyValuePair<string, string> > commandsMap = new List< KeyValuePair<string, string> >();

        private void AddCommand(string key, string value)
        {
            lock (_lock)
            {
                for (var i = 0; i < commandsMap.Count; ++i)
                {
                    if (commandsMap[i].Key == key)
                    {
                        commandsMap.RemoveAt(i);
                        break;
                    }
                }
                commandsMap.Add(new KeyValuePair<string, string>(key, value));
            }
        }

        public bool SendCommands(params MouseCommand[] command)
        {

            Task.Factory.StartNew(() =>
            {
                var receiveTime = DateTime.Now;
                if (command.Length == 2)
                {
                    if (command[0] is MouseMoveCommand firstCmd && command[1] is MouseMoveCommand secondCmd)
                    {
                        AddCommand($"MOOV {firstCmd.CommandName}+{secondCmd.CommandName}",
                            $"{firstCmd.Quantity}+{secondCmd.Quantity} {receiveTime.ToLongTimeString()}");
                    }
                }
                else if (command.Length == 1)
                {
                    var item = command[0];
                    if (item is ZoomCommand zoomCmd)
                        AddCommand($"ZOOM {zoomCmd.CommandName}", $"{zoomCmd.Quantity} {receiveTime.ToLongTimeString()}");
                    else if (item is MouseMoveCommand moveCmd)

                        AddCommand($"MOOV {moveCmd.CommandName}", $"{moveCmd.Quantity} {receiveTime.ToLongTimeString()}");
                    else
                        lock (_lock)
                            commandsMap.Clear();
                }

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            });

            if (!_isFrame)
            {
                _isFrame = true;
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                    lock (_lock)
                    {
                        foreach (var item in commandsMap)
                        {
                            System.Console.WriteLine($"{item.Key} {item.Value}");
                        }
                        commandsMap.Clear();
                    }
                    _isFrame = false;
                });
            }

            return true;
        }
        
    }
}
