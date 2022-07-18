
using Microsoft.Extensions.Logging;
using SensorPackages;
using SensorPackages.Library;

var serviceProvider = DependencyInjection.ConfigureServices();
IMainClass main = (IMainClass)serviceProvider.GetService(typeof(IMainClass));
ILogger<Program> logger = (ILogger<Program>)serviceProvider.GetService(typeof(ILogger<Program>));

try
{
    main.RunPackages();
}catch (Exception ex)
{
    logger.LogError(ex.Message);
}