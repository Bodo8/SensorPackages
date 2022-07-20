using Xunit;
using SensorPackages.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorPackages.Library.Models;
using SensorPackages.Library.Logic;
using SensorPackages.Library.Logic.Interfaces;
using NSubstitute;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace SensorPackages.Library.Services.Tests
{
    public class OutputServiceTests
    {
        [Fact()]
        public void GetOutputData_Should_ReturnStreamWithDataFromPackageMonitor()
        {
            //arrange
            var packageMonitor = new PackageMonitor("name");
            var packageHandlerStub = Substitute.For<IPackageHandler>();
            var sut = new OutputService(packageMonitor, packageHandlerStub);
            SortedList<long, Packet> paketPairs = GetPairs();
            foreach(var pair in paketPairs)
                packageMonitor.OnNext(pair.Value);

            //act
            var result = sut.GetOutputData();

            //assert
            var actual = ConvertStreamToString(result);
            actual.Should().Be("555: 1666: 1777: 1");
        }

        private SortedList<long, Packet> GetPairs()
        {
            return new SortedList<long, Packet>()
            {
                {555, new Packet(){ UnixTime = 555, StateVoltage = true} },
                {666, new Packet(){ UnixTime = 666, StateVoltage = true} },
                {777, new Packet(){ UnixTime = 777, StateVoltage = true} }
            };
        }

        public string? ConvertStreamToString(Stream stream)
        {
            if (stream == null)
                return null;

            using (var reader = new StreamReader(stream))
            {
                StringBuilder builder = new StringBuilder();

                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();

                    if (line != null)
                    {
                        builder.Append(line);
                    }
                }

                return builder.ToString();
            }
        }
    }
}