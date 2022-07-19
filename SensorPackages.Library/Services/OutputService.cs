using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Models;
using SensorPackages.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Services
{
    public class OutputService : IOutputService
    {
        private readonly IPackageMonitor _pakageMonitor;
        private readonly IPackageHandler _packageHandler;

        public OutputService(IPackageMonitor pakageMonitor, IPackageHandler packageHandler)
        {
            _pakageMonitor = pakageMonitor;
            _packageHandler = packageHandler;
        }

        public Stream GetOutputData()
        {
            SortedList<long, Packet> paketPairs = _pakageMonitor.GetOutputData();
            StringBuilder builder = new StringBuilder();

            foreach (var packet in paketPairs)
            {
                builder.Append(packet.Key.ToString());
                builder.Append(": ");
                int state = packet.Value.StateVoltage == true ? 1 : 0;
                builder.Append(state.ToString());
                builder.Append("\n");
            }
            byte[] packetData = Encoding.UTF8.GetBytes(builder.ToString());

            var stream = new MemoryStream(packetData);
            _pakageMonitor.OnCompleted();
            _packageHandler.OnCompleted();

            return stream;
        }
    }
}
