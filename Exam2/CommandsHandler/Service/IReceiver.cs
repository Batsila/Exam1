using CommandsHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommandsHandler.Service
{
    [ServiceContract]
    public interface IReceiver
    {
        [OperationContract]
        [ServiceKnownType(typeof(StopCommand))]
        [ServiceKnownType(typeof(MouseMoveCommand))]
        [ServiceKnownType(typeof(WheelCommand))]
        bool SendCommands(params MouseCommandBase[] command);
    }
}
