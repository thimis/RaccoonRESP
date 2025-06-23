using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClientLibrary.Model
{
    public static class RESPEnums
    {
        public enum ProtocolVersion
        {
            RESP2 = 2,
            RESP3 = 3
        }

        public enum DataTypeName
        {
            SimpleString,
            SimpleError,
            Integer,
            BulkString,
            Array,
            Null,
            Boolean,
            Double,
            BigNumber,
            BulkError,
            VerbatimString,
            Map,
            Attribute,
            Set,
            Push
        }

        public enum DataTypeCategory
        {
            Simple,
            Bulk,
            Aggregate,
        }
    }
}
