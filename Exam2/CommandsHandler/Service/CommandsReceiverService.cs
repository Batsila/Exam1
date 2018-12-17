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
        private const int TIMEOUT_SEC = 5;
        /// <summary>
        /// Dictionary of commands
        /// </summary>
        private static Dictionary<int, CancellationTokenSource> _processingTasks = new Dictionary<int, CancellationTokenSource>();
        private object _lock = new object();

        /// <summary>
        /// Processes requests with commands from client
        /// </summary>
        /// <param name="command">Commands from client</param>
        public bool SendCommands(params MouseCommandBase[] command)
        {
            var key = GetCommandsKey(command);

            // lock thread to avoid simultaneous access to a resource
            lock (_lock)
            {
                if (_processingTasks.ContainsKey(key))
                {
                    _processingTasks[key].Cancel();
                    _processingTasks.Remove(key);
                }

                var source = new CancellationTokenSource();
                var token = source.Token;
                _processingTasks.Add(key, source);
                Task.Factory.StartNew(() => ComandProcessingTask(command, token), token);
            }

            return true;
        }

        /// <summary>
        /// Returns hashcode of mouse command
        /// </summary>
        private int GetCommandsKey(MouseCommandBase[] command)
        {
            unchecked
            {
                int result = 37;
                result *= 397;

                for (int i = 0; i < command.Length; i++)
                    result += command[i].CommandName.GetHashCode();

                return result;
            }
        }

        /// <summary>
        /// Processes commands for TIMEOUT_SEC time frame
        /// </summary>
        private void ComandProcessingTask(MouseCommandBase[] commands, CancellationToken token)
        {
            var receiveTime = DateTime.Now;
            var sb = new StringBuilder(receiveTime.ToLongTimeString());
            sb.Append(" - ");

            if (commands.Length == 2)
            {
                if (commands[0].GetType() == typeof(MouseMoveCommand) && commands[1].GetType() == typeof(MouseMoveCommand))
                {
                    MouseMoveCommand firstCmd = (MouseMoveCommand) commands[0];
                    MouseMoveCommand secondCmd = (MouseMoveCommand)commands[1];
                    string str = string.Concat("MOOV ", firstCmd.CommandName, "+", secondCmd.CommandName, " ", firstCmd.Quantity, "+", secondCmd.Quantity);
                    sb.Append(str);

                }
            }
            else if (commands.Length == 1)
            {
                var item = commands[0];
                if (item.GetType() == typeof(WheelCommand))
                {
                    WheelCommand zoomCmd = (WheelCommand) item;
                    string str = string.Concat("Zoom ", zoomCmd.CommandName, " ", zoomCmd.Quantity);
                    sb.Append(str);
                }
                else if (item.GetType() == typeof(MouseMoveCommand))
                {
                    MouseMoveCommand moveCmd = (MouseMoveCommand) item;
                    string str = string.Concat("MOOV ", moveCmd.CommandName, " ", moveCmd.Quantity);
                    sb.Append(str);
                }
                else
                {
                    // Lock thread to avoid simultaneous access to a resource
                    lock (_lock)
                    {
                        foreach (var task in _processingTasks)
                            task.Value.Cancel();
                    }

                    sb.Append("STOP");
                    Console.WriteLine(sb.ToString());
                    return;
                }
            }

            Thread.Sleep(TimeSpan.FromSeconds(TIMEOUT_SEC));

            if (token.IsCancellationRequested)
                return;

            Console.WriteLine(sb.ToString());
        }
    }
}
