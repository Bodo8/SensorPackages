using SensorPackages.Library.Infrastructure.FileProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SensorPackages.Library.Infrastructure.Tests
{
    public class CsvProcessorTests
    {
        [Fact]
        public void GetItems_Should_ReturnListImportedObject()
        {
            //arrange
            var sut = new CsvProcessor();

            //act
            var actual = sut.GetItems(".data.csv");

            //assets
            Assert.Equal(1424, actual.Count);
        }
    }
}
