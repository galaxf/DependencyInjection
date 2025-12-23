using Autofac;
using System;

namespace DotNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //build the container
            var builer = new ContainerBuilder();

            //register types
            builer.RegisterType<WhetherForcastClient>().As<IWhetherServiceClient>();
            builer.RegisterType<WheatherForcastService>();

            //resolve dependencies
            using (var container = builer.Build())
            {
                //resolve wheather forcast service
                var wheatherService = container.Resolve<WheatherForcastService>();
                var temprature = wheatherService.GetTemprature("Pune");
                Console.WriteLine($"The temprature in Pune is {temprature}");
            }
        }
    }

    class WheatherForcastService
    {
        private readonly IWhetherServiceClient _whetherServiceClient;

        public WheatherForcastService(IWhetherServiceClient whetherServiceClient)
        {
            _whetherServiceClient = whetherServiceClient;
        }

        public double GetTemprature(string city)
        {
            return _whetherServiceClient.GetCurrentWheatherReport(city).temprature;
        }
    }

    interface IWhetherServiceClient
    {
        (string city, double temprature) GetCurrentWheatherReport(string city);
    }

    class WhetherForcastClient : IWhetherServiceClient
    {
        public (string city, double temprature) GetCurrentWheatherReport(string city)
        {
            // Simulating a call to an external weather service
            return (city, 32.5);
        }
    }
}