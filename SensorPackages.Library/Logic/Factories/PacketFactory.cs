using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Logic.Factories
{
    public class PacketFactory : IPacketFactory
    {
        public Packet CreatePacket(long unixTime, bool stateVoltage)
        {
            return new Packet(unixTime, stateVoltage);
        }
    }
}
