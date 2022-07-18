using FluentAssertions;
using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SensorPackages.Unit.Tests.Models
{
    public class PacketTests
    {
        [Fact]
        public void Create_SholdReturnPacketWithUnixTimestamp()
        {
            //arrange
            var date = new DateTimeOffset(2022, 7, 18, 18, 0, 0, TimeSpan.Zero);

            //act
            var actual = new Packet() { UnixTime = date.ToUnixTimeSeconds(), StateVoltage = true };

            //assert
            actual.Should().BeOfType<Packet>();
            actual.UnixTime.Should().Be(1658167200);
        }
    }
}
