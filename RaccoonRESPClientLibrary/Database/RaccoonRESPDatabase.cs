using RaccoonRESPClientLibrary.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClientLibrary.Database
{
    public class RaccoonRESPDatabase
    {
        public RaccoonRESPClient Client { get; set; }
        public DatabaseString String { get; set; }
    }
}
