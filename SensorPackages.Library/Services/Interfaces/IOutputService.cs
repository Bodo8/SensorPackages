using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Services.Interfaces
{
    public interface IOutputService
    {
        Stream GetOutputData();
    }
}
