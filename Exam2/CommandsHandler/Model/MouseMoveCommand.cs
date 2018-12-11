using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommandsHandler.Model
{
    [DataContract]
    public class MouseMoveCommand : MouseCommandBase
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
