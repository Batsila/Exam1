﻿using CommandsHandler.Model;
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
        private static Dictionary<int, CancellationTokenSource> _processingTasks = new Dictionary<int, CancellationTokenSource>();
        private object _lock = new object();

        public bool SendCommands(params MouseCommandBase[] command)
        {
            var key = GetCommandsKey(command);

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

        private void ComandProcessingTask(MouseCommandBase[] commands, CancellationToken token)
        {
            var receiveTime = DateTime.Now;
            var sb = new StringBuilder(receiveTime.ToLongTimeString());
            sb.Append(" - ");

            if (commands.Length == 2)
            {
                if (commands[0] is MouseMoveCommand firstCmd && commands[1] is MouseMoveCommand secondCmd)
                    sb.Append($"MOOV {firstCmd.CommandName}+{secondCmd.CommandName} {firstCmd.Quantity}+{secondCmd.Quantity}");
            }
            else if (commands.Length == 1)
            {
                var item = commands[0];
                if (item is WheelCommand zoomCmd)
                    sb.Append($"ZOOM {zoomCmd.CommandName} {zoomCmd.Quantity}");
                else if (item is MouseMoveCommand moveCmd)
                    sb.Append($"MOOV {moveCmd.CommandName} {moveCmd.Quantity}");
                else
                {
                    lock (_lock)
                    {
                        foreach (var task in _processingTasks)
                            task.Value.Cancel();
                    }

                    sb.Append($"STOP");
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
