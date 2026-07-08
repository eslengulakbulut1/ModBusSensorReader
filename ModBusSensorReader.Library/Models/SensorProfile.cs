using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSensorReader.Library.Models
{
    public class SensorProfile
    {
        public string? SensorName { get; set; }
        public byte SlaveId { get; set; }

        // Sıcaklık değerlerini okumak için kullanılacak register adresi, birim katsayısı ve birim türü
        public int TemperatureRegister { get; set; }
        public double TemperatureCoefficient { get; set; }
        public int TemperatureUnit { get; set; }

        // Nem değerlerini okumak için kullanılacak register adresi, birim katsayısı ve birim türü
        public int HumidityRegister { get; set; }
        public double HumidityCoefficient { get; set; }
        public int HumidityUnit { get; set; }

        // Basınç değerlerini okumak için kullanılacak register adresi, birim katsayısı ve birim türü
        public int PressureRegister { get; set; }
        public double PressureCoefficient { get; set; }
        public int PressureUnit { get; set; }

    }
}
