
using Microsoft.Extensions.Logging;
using SensorPackages.Library.Logic;
using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library
{
    public class MainClass : IMainClass
    {
        private readonly IInputService _inputService;
        private readonly IPackageHandler _packageHandler;
        private readonly ILogger<MainClass> _logger;

        public MainClass(IInputService inputService, IPackageHandler packageHandler, ILogger<MainClass> logger)
        {
            _inputService = inputService;
            _packageHandler = packageHandler;
            _logger = logger;
        }

        public bool RunPackages()
        {
            try
            {
                _logger.LogInformation("Start app");
                Stream inputData = GetData();
                PackageMonitor observer = new PackageMonitor("observer");
                _packageHandler.Subscribe(observer);
                _inputService.ConvertStreamToPackages(inputData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("App fished");

            return true;
        }

        private Stream GetData()
        {
            return File.OpenRead(".input.txt");
        }
    }
}
