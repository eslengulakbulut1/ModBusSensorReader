using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sensör profili oluşturmak için kurulan model sınıfı.  

namespace ModbusSensorReader.Library.Models
{
    public class SensorProfile
    {
        public string SensorName { get; set; }
        public byte SlaveId { get; set; }

        public List<SensorParameter> Parameters { get; set; } = new List<SensorParameter>();
    }
}
