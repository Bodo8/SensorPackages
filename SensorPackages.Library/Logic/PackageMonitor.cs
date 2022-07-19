using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Logic
{
    public class PackageMonitor : IPackageMonitor
    {
        public string Name { get; private set; }
        private readonly SortedList<long, Packet> _packets;
        IDisposable cancellation;

        public PackageMonitor(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("The observer must be assigned a name.");

            Name = name;
            _packets = new SortedList<long, Packet>();
        }

        public void OnNext(Packet packet)
        {
            if (!_packets.ContainsKey((long)packet.UnixTime))
            {
                _packets.Add((long)packet.UnixTime, packet);
            }
        }

        public virtual void Subscribe(IObservable<Packet> provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
        }

        public void OnCompleted()
        {
            _packets.Clear();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public SortedList<long, Packet> GetOutputData()
        {
            return _packets;
        }
    }
}
