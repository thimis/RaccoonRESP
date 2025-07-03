using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.Core
{
    public class RaccoonRESPCommands
    {
        public RaccoonRESPClient Client { get; set; }
        public RaccoonRESPStringCommands String { get; set; }
        public RaccoonRESPTransactionCommands Transaction { get; set; }
    }

}
