using CommandsHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandsHandler.Service
{
    public class CommandsReceiverService : IReceiver
    {
        private static object _lock;
        private const int TIMEOUT_SEC = 5;
        private static List<KeyValuePair<string, string>> _commands;

        static CommandsReceiverService()
        {
            _commands = new List<KeyValuePair<string, string>>();
            _lock = new object();
            // Create new task for printing commands
            Task.Factory.StartNew(() => PrintCommandTask());
        }

        /// <summary>
        /// The thread that processes requests with commands from client. It is started by hosting environment.
        /// </summary>
        /// <param name="command">Commands from client</param>
        public bool SendCommands(params MouseCommandBase[] commands)
        {
            var receiveTime = DateTime.Now;
            var key = string.Empty;
            var sb = new StringBuilder(receiveTime.ToLongTimeString());
            sb.Append(" - ");

            if (commands.Length == 2)
            {
                if (commands[0] is MouseMoveCommand && commands[1] is MouseMoveCommand)
                {
                    var firstCmd = (MouseMoveCommand)commands[0];
                    var secondCmd = (MouseMoveCommand)commands[1];
                    key = string.Format("MOOV {0}+{1}", firstCmd.CommandName, secondCmd.CommandName);
                    string str = string.Format("{0} {1}+{2}", key, firstCmd.Quantity, secondCmd.Quantity);
                    sb.Append(str);
                }
            }
            else if (commands.Length == 1)
            {
                var item = commands[0];
                if (item is WheelCommand)
                {
                    var zoomCmd = (WheelCommand)item;
                    key = string.Format("ZOOM {0}", zoomCmd.CommandName);
                    string str = string.Format("{0} {1}", key, zoomCmd.Quantity);
                    sb.Append(str);
                }
                else if (item is MouseMoveCommand)
                {
                    var moveCmd = (MouseMoveCommand)item;
                    key = string.Format("MOOV {0}", moveCmd.CommandName);
                    string str = string.Format("{0} {1}", key, moveCmd.Quantity);
                    sb.Append(str);
                }
                else
                {
                    key = "STOP";
                    sb.Append(key);
                }
            }

            AddCommand(key, sb.ToString());

            return true;
        }

        /// <summary>
        /// React to the requests and manage a smart queue, update and delete data from the queue.
        /// </summary>
        private void AddCommand(string key, string commandValue)
        {
            // Lock thread to avoid simultaneous access to a resource
            lock (_lock)
            {
                if (key == "STOP")
                {
                    // Once a STOP command has arrived, remove all commands from the queue (clear queue)
                    _commands.Clear();
                    _commands.Add(new KeyValuePair<string, string>(key, commandValue));
                    return;
                }

                var indexOfStopCommand = _commands.IndexOf(_commands.Find(x => x.Key == "STOP"));
                if (indexOfStopCommand >= 0)
                {
                    //If a STOP command has received followed by a new one (any type), the new command replace the STOP command.
                    _commands.RemoveAt(indexOfStopCommand);
                    _commands.Insert(indexOfStopCommand, new KeyValuePair<string, string>(key, commandValue));
                }
                else
                {
                    var index = _commands.IndexOf(_commands.Find(x => x.Key == key));
                    if (index >= 0)
                    {
                        // If command already exists in list remove this command and and new in previous position
                        _commands.RemoveAt(index);
                        _commands.Insert(index, new KeyValuePair<string, string>(key, commandValue));
                    }
                    else
                    {
                        // If command does not already exist in list add new to the end of list
                        _commands.Add(new KeyValuePair<string, string>(key, commandValue));
                    }
                }
            }
        }

        /// <summary>
        /// Takes the next command in the queue and execute it (printing the command and then 5 seconds hold)
        /// </summary>
        private static void PrintCommandTask()
        {
            while (true)
            {
                // Lock thread to avoid simultaneous access to a resource
                lock (_lock)
                {
                    // Check if any command exists in the queue
                    if (_commands.Any())
                    {
                        // Take first command from the queue and print it
                        System.Console.WriteLine(_commands[0].Value);
                        _commands.RemoveAt(0);
                    }
                }
                // 5 seconds hold
                Thread.Sleep(TimeSpan.FromSeconds(TIMEOUT_SEC));
            }
        }
    }
}
