using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommandsHandler.Model
{
    [DataContract]
    [KnownType(typeof(StopCommand))]
    [KnownType(typeof(MouseMoveCommand))]
    [KnownType(typeof(WheelCommand))]
    public abstract class MouseCommandBase
    {
        [DataMember]
        public abstract string CommandName { get; set; }
    }
}
