using SensorPackages.Library.Logic.Factories;
using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Models;
using SensorPackages.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Services
{
    public class InputService : IInputService
    {
        private readonly IPackageHandler _packageHandler;
        private readonly IPacketFactory _packetFactory;

        public InputService(IPackageHandler packageHandler, IPacketFactory packetFactory)
        {
            _packageHandler = packageHandler;
            _packetFactory = packetFactory;
        }

        public void ConvertStreamToPackages(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    Packet packet = new();
                    string? line = reader.ReadLine();

                    if (line != null)
                    {
                        string[] parts = line.Split(":");

                        if (parts.Length > 1)
                        {
                            bool voltage = parts[1].Trim() == "1" ? true : false;
                            packet = _packetFactory.CreatePacket(long.Parse(parts[0].Trim()), voltage);
                        }
                    }
                    _packageHandler.AddPacket(packet);
                }
            }
        }
    }
}
