using Xunit;
using SensorPackages.Library.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorPackages.Library.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SensorPackages.Library.Logic.Interfaces;

namespace SensorPackages.Library.Logic.Tests
{
    public class PackageHandlerTests
    {
        private readonly PackageHandler _sut;
        private readonly ILogger<PackageHandler> _loggerMock;
        private readonly IPackageMonitor _observerMock;

        public PackageHandlerTests()
        {
            _loggerMock = Substitute.For<ILogger<PackageHandler>>();
            _observerMock = Substitute.For<IPackageMonitor>();
            _sut = new PackageHandler(_loggerMock);
        }

        [Fact()]
        public void Subscribe_Should_CallToObserversOnes()
        {
            //arrange
            Packet packet = new(1615560000, false);
            _sut.AddPacket(packet);

            //act
            _sut.Subscribe(_observerMock);

            //assert
            _observerMock.Received(1).OnNext(packet);
        }

        [Fact()]
        public void AddPacket_Should_CallToObserversOnes()
        {
            //arrange;
            Packet packet = new(1615560000, false);
            _sut.Subscribe(_observerMock);

            //act
            _sut.AddPacket(packet);

            //assert
            _observerMock.Received(1).OnNext(packet);
        }

        [Theory()]
        [MemberData(nameof(DataPacket))]
        public void FindChangeInPacket_Should_ReturnExpect(List<Packet> packets, bool[] expects)
        {
            for (var i = 0; i < packets.Count; i++)
            {
                //arrange;
                _sut.AddPacket(packets[i]);

                //act
                var actual = _sut.FindChangeInPacket(packets[i]);

                //assert
                actual.Should().Be(expects[i]);
            }
        }

        private static IEnumerable<object[]> DataPacket()
        {
            yield return new object[] { new List<Packet> {
            new() { UnixTime = 1615560000, StateVoltage = true},
            new() { UnixTime = 1615560020, StateVoltage = true},
            new() { UnixTime = 1615560025, StateVoltage = false},
            new() { UnixTime = 1615560032, StateVoltage = false},
            new() { UnixTime = 1615560045, StateVoltage = true},
            new() { UnixTime = 1615560042, StateVoltage = true},
            new() { UnixTime = 1615560050, StateVoltage = true},
            new() { UnixTime = 1615560070, StateVoltage = true},
            new() { UnixTime = 1615560060, StateVoltage = true},
            },
            new bool[] { true, false, true, false, true, false, false, false, false }
            };

        }
    }
}