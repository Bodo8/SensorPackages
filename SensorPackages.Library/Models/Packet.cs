using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Models
{
    public class Packet : IEquatable<Packet>
    {
        public Packet()
        {
        }

        public Packet(long unixTime, bool stateVoltage)
        {
            UnixTime = unixTime;
            StateVoltage = stateVoltage;
        }

        public long UnixTime { get; set; }
        public bool StateVoltage { get; set; }

        public bool Equals(Packet? other)
        {
            if (this == null || other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (UnixTime == other.UnixTime
                && StateVoltage == other.StateVoltage)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UnixTime, StateVoltage);
        }
    }
}
