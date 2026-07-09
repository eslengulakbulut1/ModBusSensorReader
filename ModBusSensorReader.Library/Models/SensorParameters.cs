using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sensöre eklenecek parametreler için oluşturulmuş model sınıfı.
namespace ModbusSensorReader.Library.Models
{
    public class SensorParameter
    {
        public string ParameterName { get; set; }
        public int RegisterAddress { get; set; }
        public double Coefficient { get; set; } 
        public string Unit { get; set; }
        public string RawValue { get; set; }
        public string CalculatedValue { get; set; }
    }
}
