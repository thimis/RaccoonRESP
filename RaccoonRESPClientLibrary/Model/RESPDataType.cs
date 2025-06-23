using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaccoonRESPClientLibrary.Model.RESPEnums;

namespace RaccoonRESPClientLibrary.Model
{
    public class RESPDataType
    {
        public DataTypeName Name { get; set; }
        public ProtocolVersion MinimalProtocolVersion { get; set; }
        public DataTypeCategory Category { get; set; }
        public string FirstByte { get; set; }
    }
}
