﻿
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SensorPackages.Library;
using SensorPackages.Library.Common;
using SensorPackages.Library.Logic;
using SensorPackages.Library.Logic.Factories;
using SensorPackages.Library.Logic.Interfaces;
using SensorPackages.Library.Models.ConfigurationModels;
using SensorPackages.Library.Services;
using SensorPackages.Library.Services.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages
{
    public class DependencyInjection
    {
        public static IServiceProvider ConfigureServices()
        {
            var config = new ConfigurationBuilder()
                             .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                             .Build();

            MapperConfiguration mapperConfig = GetMapperConfig();
            IMapper mapper = mapperConfig.CreateMapper();

            var serviceCollection = new ServiceCollection()
                                       .AddLogging(loggingBuilder =>
                                       {
                                           loggingBuilder.ClearProviders();
                                           loggingBuilder.SetMinimumLevel(LogLevel.Information);
                                           loggingBuilder.AddNLog(config);
                                       })
                                       .AddSingleton<IConfiguration>(config)
                                       .AddSingleton(mapper)
                                       .AddSingleton<IMainClass, MainClass>()
                                       .AddScoped<IInputService, InputService>()
                                       .AddScoped<IPackageHandler, PackageHandler>()
                                       .AddScoped<IOutputService, OutputService>()
                                       .AddTransient<IPacketFactory, PacketFactory>()
                                       ;

            NLog.LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
            serviceCollection.Configure<AppSettings>(options => config.GetSection("AppSettings").Bind(options));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private static MapperConfiguration GetMapperConfig()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
        }
    }
}
