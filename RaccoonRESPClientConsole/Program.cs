using RaccoonRESPClientLibrary.Client;
using RaccoonRESPClientLibrary.Model;
using static RaccoonRESPClientLibrary.Model.RESPEnums;

namespace RaccoonRESPClientConsole
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            var connection = new RaccoonRESPClientLibrary.Connection.RaccoonRESPConnection();

            var client = new RaccoonRESPClient(connection);
            //Connect to Redis Server
            await client.ConnectAsync();

            var db = client.GetDatabase();

            db.String.Set("TimeKey3", "somevalue");

            var value = db.String.Get("TimeKey3");


            //Send Hello Command
            //var helloResponse = client.SendCommandAsync($"HELLO 3 AUTH default mypassword").Result;

            ////Start Transaction
            //var transactionResponse = await client.SendCommandAsync("MULTI");
            //var setResponse = client.SendCommandAsync($"SET CoolTestKey \"somecooltestvalue\"").Result;
            //var getResponse = client.SendCommandAsync($"GET CoolTestKey").Result;
            ////End Transaction
            //var execResponse = client.SendCommandAsync("EXEC").Result;

            

            //var setResponse = client.SendCommandAsync($"SET TimeKey3 somevalue").Result;

            //var getResponse = client.SendCommandAsync($"GET TimeKey3").Result;

            //var existsResponse = client.SendCommandAsync($"EXISTS TimeCrazyKey").Result;

            //var dataTypes = CreateRESPDataTypes();

            //var resoonseLetters = getResponse.ToCharArray().ToList();

            //var dataType = dataTypes.FirstOrDefault(dt => dt.FirstByte == resoonseLetters[0].ToString());
        }

        private static List<RESPDataType> CreateRESPDataTypes()
        {
            var dataTypelist = new List<RESPDataType>();
            var dataTypeEnums = Enum.GetValues(typeof(DataTypeName)).Cast<DataTypeName>();

            foreach (var dataTypeEnum in dataTypeEnums)
            {
                var dataType = new RESPDataType();

                switch (dataTypeEnum)
                {
                    case DataTypeName.SimpleString:
                        dataType.Name = DataTypeName.SimpleString;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP2;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = "+";
                        break;
                    case DataTypeName.SimpleError:
                        dataType.Name = DataTypeName.SimpleError;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP2;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = "-";
                        break;
                    case DataTypeName.Integer:
                        dataType.Name = DataTypeName.Integer;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP2;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = ":";
                        break;
                    case DataTypeName.BulkString:
                        dataType.Name = DataTypeName.BulkString;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP2;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "$";
                        break;
                    case DataTypeName.Array:
                        dataType.Name = DataTypeName.Array;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP2;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "*";
                        break;
                    case DataTypeName.Null:
                        dataType.Name = DataTypeName.Null;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = "_";
                        break;
                    case DataTypeName.Boolean:
                        dataType.Name = DataTypeName.Boolean;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = "#";
                        break;
                    case DataTypeName.Double:
                        dataType.Name = DataTypeName.Double;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = ",";
                        break;
                    case DataTypeName.BigNumber:
                        dataType.Name = DataTypeName.BigNumber;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Simple;
                        dataType.FirstByte = "(";
                        break;
                    case DataTypeName.BulkError:
                        dataType.Name = DataTypeName.BulkError;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "!";
                        break;
                    case DataTypeName.VerbatimString:
                        dataType.Name = DataTypeName.VerbatimString;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "=";
                        break;
                    case DataTypeName.Map:
                        dataType.Name = DataTypeName.Map;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "%";
                        break;
                    case DataTypeName.Attribute:
                        dataType.Name = DataTypeName.Attribute;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "`";
                        break;
                    case DataTypeName.Set:
                        dataType.Name = DataTypeName.Set;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = "~";
                        break;
                    case DataTypeName.Push:
                        dataType.Name = DataTypeName.Push;
                        dataType.MinimalProtocolVersion = ProtocolVersion.RESP3;
                        dataType.Category = DataTypeCategory.Aggregate;
                        dataType.FirstByte = ">";
                        break;
                    default:
                        break;
                }
                dataTypelist.Add(dataType);
            }

            return dataTypelist;
        }
    }
}
