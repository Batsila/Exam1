using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommandsHandler
{
    [ServiceContract]
    public interface IReceiver
    {
        [OperationContract]
        [ServiceKnownType(typeof(StopCommand))]
        [ServiceKnownType(typeof(MouseMoveCommand))]
        bool SendCommands(params MouseCommand[] command);
    }

    [DataContract]
    [KnownType(typeof(StopCommand))]
    [KnownType(typeof(MouseMoveCommand))]
    public abstract class MouseCommand
    {
        [DataMember]
        public abstract string CommandName { get; set; }
    }

    [DataContract]
    public class StopCommand : MouseCommand
    {
        [DataMember]
        public override string CommandName
        {
            get => "STOP";
            set { }
        }
    }

    [DataContract]
    public class MouseMoveCommand : MouseCommand
    {
        [DataMember]
        public double Quantity { get; set; }

        [DataMember]
        public override string CommandName { get; set; }

        public MouseMoveCommand(string name, double quantity)
        {
            CommandName = name;
            Quantity = quantity;
        }
    }
}
