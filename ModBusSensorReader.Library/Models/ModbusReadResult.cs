using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSensorReader.Library.Models
{
    public class ModbusReadResult
    {
        public ushort[] Registers { get; set; } = Array.Empty<ushort>();
        public string RawText { get; set; } = "-";
        public string ParsedValue { get; set; } = "-";
        public DateTime ReadTime { get; set; } = DateTime.Now;
    }
}

