using Microsoft.Extensions.Logging;
using NSubstitute;
using SensorPackages.Library;
using SensorPackages.Library.Logic;
using SensorPackages.Library.Logic.Factories;
using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Services;
using SensorPackages.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SensorPackages.Tests.Integration
{
    public class MainClassTests
    {
        private readonly IInputService _inputService;
        private readonly MainClass _sut;

        public MainClassTests()
        {
            var loggerStub = Substitute.For<ILogger<MainClass>>();
            var loggerHandlerStub = Substitute.For<ILogger<PackageHandler>>();
            var packageHandler = new PackageHandler(loggerHandlerStub);
            var factory = new PacketFactory();
            _inputService = new InputService(packageHandler, factory);
            _sut = new MainClass(_inputService, packageHandler, loggerStub);
        }

        [Fact]
        public void RunPackages_Should_ReturnTrue()
        {
            //act
            var actual = _sut.RunPackages();

            //assert
            Assert.True(actual);
        }
    }
}
