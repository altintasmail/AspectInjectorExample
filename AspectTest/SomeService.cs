using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectTest
{
    public interface ISomeService
    {
        void Hello();
    }
    
    public class SomeService : ISomeService
    {
        [Log]
        [Cache]
        [Validation]
        public void Hello()
        {
            Console.WriteLine("Orjinal Metod içi: Merhaba Dünya");
        }
    }
}
