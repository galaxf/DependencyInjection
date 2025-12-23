# Dependency Injection Tutorial - .NET Project

A comprehensive tutorial project demonstrating Dependency Injection (DI) patterns in both .NET Core and .NET Framework. This project shows how to implement DI using two different approaches: Microsoft's built-in DI container and Autofac.

## 📋 Project Overview

This project contains two implementations of the same weather forecasting application:

1. **DotNetCore** - Uses Microsoft.Extensions.DependencyInjection (built-in DI)
2. **DotNetFramework** - Uses Autofac (third-party DI container)

Both implementations demonstrate the same concept: decoupling services from their implementations through dependency injection.

## 🎯 What is Dependency Injection?

Dependency Injection is a design pattern that promotes loose coupling and increases code maintainability by:
- Removing hard dependencies between classes
- Making code testable and mockable
- Following the Dependency Inversion Principle
- Improving code reusability

## 📁 Project Structure

```
DependencyInjection/
├── DotNetCore/
│   ├── Program.cs              # .NET Core implementation with built-in DI
│   └── DotNetCore.csproj       # .NET Core project file (targets net10.0)
├── DotNetFramework/
│   ├── Program.cs              # .NET Framework implementation with Autofac
│   ├── App.config              # Application configuration
│   ├── DotNetFramework.csproj
│   └── packages.config         # NuGet package references
├── packages/                    # External packages directory
├── .gitignore                   # Git ignore rules
└── DependencyInjection.slnx     # Solution file
```

## 🚀 Getting Started

### Prerequisites

- .NET 10.0 SDK (for DotNetCore project)
- .NET Framework 4.x (for DotNetFramework project)
- Visual Studio 2022 or VS Code with C# extension

### Running the Projects

#### DotNetCore Project

```bash
cd DotNetCore
dotnet run
```

**Output:**
```
The temprature in Pune is 32.5
```

#### DotNetFramework Project

```bash
cd DotNetFramework
dotnet run
```

**Output:**
```
The temprature in Pune is 32.5
```

## 💡 Implementation Examples

### 1. Microsoft.Extensions.DependencyInjection (.NET Core)

#### Service Registration

```csharp
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

// Register service and its implementation as Transient
serviceCollection.AddTransient<IWhetherServiceClient, WhetherForcastClient>();
serviceCollection.AddTransient<WheatherForcastService>();

// Build the service provider
using(var service = serviceCollection.BuildServiceProvider())
{
    var provider = service.GetRequiredService<WheatherForcastService>();
    var temprature = provider.GetTemprature("Pune");
    Console.WriteLine($"The temprature in Pune is {temprature}");
}
```

#### Interface Definition

```csharp
interface IWhetherServiceClient
{
    (string city, double temprature) GetCurrentWheatherReport(string city);
}
```

#### Implementation

```csharp
class WhetherForcastClient : IWhetherServiceClient
{
    public (string city, double temprature) GetCurrentWheatherReport(string city)
    {
        // Simulating a call to an external weather service
        return (city, 32.5);
    }
}
```

#### Service Using Dependencies

```csharp
class WheatherForcastService
{
    private readonly IWhetherServiceClient _whetherServiceClient;

    // Dependency is injected through constructor
    public WheatherForcastService(IWhetherServiceClient whetherServiceClient)
    {
        _whetherServiceClient = whetherServiceClient;
    }

    public double GetTemprature(string city)
    {
        return _whetherServiceClient.GetCurrentWheatherReport(city).temprature;
    }
}
```

### 2. Autofac (.NET Framework)

#### Container Registration and Resolution

```csharp
using Autofac;
using System;

// Create container builder
var builder = new ContainerBuilder();

// Register types
builder.RegisterType<WhetherForcastClient>().As<IWhetherServiceClient>();
builder.RegisterType<WheatherForcastService>();

// Build the container
using (var container = builder.Build())
{
    // Resolve dependencies
    var wheatherService = container.Resolve<WheatherForcastService>();
    var temprature = wheatherService.GetTemprature("Pune");
    Console.WriteLine($"The temprature in Pune is {temprature}");
}
```

#### Key Differences from Microsoft DI

- Uses `ContainerBuilder` instead of `ServiceCollection`
- Uses `Resolve<T>()` instead of `GetRequiredService<T>()`
- More advanced features like modules, scanning, and interceptors
- Better support for property injection

## 📦 Dependencies

### DotNetCore Project

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="10.0.1" />
</ItemGroup>
```

### DotNetFramework Project

```xml
Autofac (v9.0.0)
Autofac.Extensions.DependencyInjection (v10.0.0)
Microsoft.Extensions.DependencyInjection.Abstractions (v8.0.1)
```

## 🔄 Service Lifetimes

Both frameworks support different service lifetimes:

### Microsoft.Extensions.DependencyInjection

```csharp
// Transient - New instance every time
serviceCollection.AddTransient<IService, Service>();

// Scoped - New instance per scope (common in web apps)
serviceCollection.AddScoped<IService, Service>();

// Singleton - Single instance for application lifetime
serviceCollection.AddSingleton<IService, Service>();
```

### Autofac

```csharp
// Transient (default)
builder.RegisterType<Service>().As<IService>();

// Instance per lifetime scope
builder.RegisterType<Service>().As<IService>().InstancePerLifetimeScope();

// Single instance
builder.RegisterType<Service>().As<IService>().SingleInstance();
```

## 🧪 Testing with Dependency Injection

A major benefit of DI is easier unit testing. You can create mock implementations:

```csharp
// Mock implementation for testing
class MockWhetherServiceClient : IWhetherServiceClient
{
    public (string city, double temprature) GetCurrentWheatherReport(string city)
    {
        return (city, 25.0); // Fixed temperature for testing
    }
}

// Use in tests
var mockClient = new MockWhetherServiceClient();
var service = new WheatherForcastService(mockClient);
var temp = service.GetTemprature("TestCity");
Assert.Equal(25.0, temp);
```

## 🏗️ Building the Project

### Using VS Code Tasks

```bash
# Build the project
Ctrl+Shift+B (or run: dotnet build)

# Publish the project
dotnet publish

# Watch for changes
dotnet watch run
```

### Using Command Line

```bash
# DotNetCore
dotnet build DotNetCore/DotNetCore.csproj
dotnet publish DotNetCore/DotNetCore.csproj

# DotNetFramework
dotnet build DotNetFramework/DotNetFramework.csproj
```

## 📚 Learning Outcomes

By studying this project, you'll understand:

✅ What Dependency Injection is and why it matters  
✅ How to use Microsoft's built-in DI container  
✅ How to use Autofac for advanced DI scenarios  
✅ Service registration and lifetime management  
✅ Constructor injection patterns  
✅ Loose coupling and SOLID principles  
✅ How to make code more testable  

## 🔗 Related Concepts

- **Constructor Injection** - Passing dependencies through constructor
- **Service Locator Pattern** - Anti-pattern; DI is preferred
- **Factory Pattern** - Alternative approach for object creation
- **SOLID Principles** - Dependency Inversion Principle especially relevant

## 📖 Additional Resources

- [Microsoft.Extensions.DependencyInjection Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection)
- [Autofac Documentation](https://autofac.readthedocs.io/)
- [ASP.NET Core Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)

## 📝 Notes

- Both implementations achieve the same result using different DI containers
- The weather data is simulated and always returns 32.5°C
- This is a console application demonstrating core DI concepts
- For production scenarios, consider ASP.NET Core which has built-in DI support

## 🤝 Contributing

Feel free to enhance this tutorial by:
- Adding more service examples
- Implementing different lifetime patterns
- Adding property injection examples
- Creating unit tests
- Adding error handling patterns

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

The MIT License is a permissive open-source license that allows you to:
- ✅ Use this project for commercial purposes
- ✅ Modify the code
- ✅ Distribute the software
- ✅ Use it privately

The only requirement is to include a copy of the license and copyright notice.

---

**Last Updated:** December 2025
