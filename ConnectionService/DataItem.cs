using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DataItem
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string Vendor { get; set; }

        public string Model { get; set; }

        public bool IsOnline { get; set; }
    }
}
