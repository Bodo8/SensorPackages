
using Microsoft.Extensions.Logging;
using SensorPackages;
using SensorPackages.Library;

var serviceProvider = DependencyInjection.ConfigureServices();
IMainClass main = (IMainClass)serviceProvider.GetService(typeof(IMainClass));

main.RunPackages();
