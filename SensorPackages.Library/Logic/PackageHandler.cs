using Microsoft.Extensions.Logging;
using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Models;
using SensorPackages.Library.Services.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Logic
{
    public class PackageHandler : IPackageHandler
    {
        private readonly List<IObserver<Packet>> _observers;
        private readonly ILogger<PackageHandler> _logger;
        private readonly SortedList<long, Packet> _sortedPackets;
        private readonly List<Packet> _packets;

        public PackageHandler(ILogger<PackageHandler> logger)
        {
            _observers = new List<IObserver<Packet>>();
            _sortedPackets = new SortedList<long, Packet>();
            _packets = new List<Packet>();
            _logger = logger;
        }

        public void AddPacket(Packet packet)
        {
            if (packet.UnixTime !=null  && !_sortedPackets.ContainsKey((long)packet.UnixTime))
            {
                _sortedPackets.Add((long)packet.UnixTime, packet);
                bool change = FindChangeInPacket(packet);

                if (change)
                {
                    _packets.Add(packet);

                    foreach (var observer in _observers)
                    {
                        observer.OnNext(packet);
                        _logger.LogInformation("Observer {Name} received" +
                            "change packet - {UnixTime}: {StateVoltage}",
                            observer.GetType().Name, packet.UnixTime, packet.StateVoltage);
                    }
                }
            }
        }

        public bool FindChangeInPacket(Packet packet)
        {
            if (_sortedPackets.Count > 1
                && _sortedPackets.Last().Value.UnixTime == packet.UnixTime)
                return _sortedPackets.Values[_sortedPackets.Count() - 2].StateVoltage != packet.StateVoltage;

            if (_sortedPackets.Count > 0
                && _sortedPackets.Last().Value.UnixTime == packet.UnixTime)
                return true;

            return false;
        }

        public IDisposable Subscribe(IObserver<Packet> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);

                foreach (var item in _packets)
                    observer.OnNext(item);
            }

            return new Unsubscriber<Packet>(_observers, observer);
        }

        public void OnCompleted()
        {
            Packet last = _sortedPackets.Last().Value;
            _sortedPackets.Clear();

            if(last.UnixTime != null)
               _sortedPackets.Add((long)last.UnixTime, last);
        }
    }
}
