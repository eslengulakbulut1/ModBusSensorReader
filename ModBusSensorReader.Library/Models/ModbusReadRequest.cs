using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSensorReader.Library.Models
{
    public class ModbusReadRequest
    {
        public string ConnectionType { get; set; } = "RTU"; // Bağlantı Türü RTU

        public string PortName { get; set; } = "COM1"; // Port Adı
        public int BaudRate { get; set; } = 9600; // Baud Rate
        public int DataBits { get; set; } = 8;
        public string Parity { get; set; } = "None";
        public string StopBits { get; set; } = "One";

        public string IpAddress { get; set; } = "127.0.0.1";
        public int TcpPort { get; set; } = 502;

        public byte SlaveId { get; set; } = 1;
        public int FunctionCode { get; set; } = 3;
        public ushort StartAddress { get; set; } = 0;
        public ushort RegisterCount { get; set; } = 1;
        public string DataType { get; set; } = "UInt16";
        public ushort WriteAddress { get; set; }
        public string WriteValue { get; set; } = "0";
        public string WriteDataType { get; set; } = "UInt16";
        public string WriteAddressText { get; set; } = "0";

    }
}

