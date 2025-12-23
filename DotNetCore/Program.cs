
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<IWhetherServiceClient, WhetherForcastClient>();
serviceCollection.AddTransient<WheatherForcastService>();

using(var service = serviceCollection.BuildServiceProvider())
{
    var provider = service.GetRequiredService<WheatherForcastService>();
    var temprature = provider.GetTemprature("Pune");
    Console.WriteLine($"The temprature in Pune is {temprature}");
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