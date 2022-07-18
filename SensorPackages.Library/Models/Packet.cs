using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Models
{
    public class Packet
    {
        public long UnixTime { get; set; }
        public bool StateVoltage { get; set; }
    }
}
