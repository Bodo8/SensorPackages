using Xunit;
using SensorPackages.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SensorPackages.Library.Models;
using FluentAssertions;
using NSubstitute;
using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Logic.Factories;

namespace SensorPackages.Library.Services.Tests
{
    public class InputServiceTests
    {
        [Fact()]
        public void ConvertStreamToPackages_Should_Return_CallOneToAddPacket()
        {
            //arrange
            var packageHandlerStub = Substitute.For<IPackageHandler>();
            var factoryStub = Substitute.For<IPacketFactory>();
            var sut = new InputService(packageHandlerStub, factoryStub);
            var lines = File.OpenRead(".input2.txt");
            var packet = factoryStub.CreatePacket(1615560000, true);
            factoryStub.CreatePacket(1615560000, true).Returns(packet);

            //act
            sut.ConvertStreamToPackages(lines);

            //assert
            packageHandlerStub.Received(2).AddPacket(packet);
        }
    }
}