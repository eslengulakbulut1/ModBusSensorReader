using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSensorReader.Library.Models
{
    public class ExcelRecord
    {
        public DateTime ReadingTime { get; set; }
        public string SensorName { get; set; } = string.Empty;
        public byte SlaveId { get; set; } 
        public string ParameterName { get; set; } = string.Empty;
        public int RegisterAddress { get; set; }
        public ushort RawValue { get; set; }
        public double CalculatedValue { get; set; }
        public string Unit { get; set; } = string.Empty;

    }
}
