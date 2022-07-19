using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Logic.Interfaces
{
    public interface IPackageMonitor : IObserver<Packet>
    {
        SortedList<long, Packet> GetOutputData();
    }
}
