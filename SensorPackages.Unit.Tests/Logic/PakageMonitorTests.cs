using Xunit;
using SensorPackages.Library.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorPackages.Library.Models;
using FluentAssertions;

namespace SensorPackages.Library.Logic.Tests
{
    public class PakageMonitorTests
    {
        private readonly PackageMonitor _sut;

        public PakageMonitorTests()
        {
            _sut = new PackageMonitor("monitor");
        }

        [Fact()]
        public void OnNext_Should_AddPacketToList()
        {
            //arrange
            Packet packet = new(1615560000, false);
            
            //act
            _sut.OnNext(packet);

            //assert
            _sut.GetOutputData().Should().HaveCount(1);
            _sut.GetOutputData().First().Value.UnixTime.Should().Be(1615560000);
        }
    }
}