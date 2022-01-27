using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<ISomeService, SomeService>()
            .BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ISomeService>();
            Console.WriteLine(service.Hello());
            Console.ReadLine();
        }
    }
}
