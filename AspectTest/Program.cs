using System;

namespace AspectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SomeService();
            service.Hello();
        }
    }
}
