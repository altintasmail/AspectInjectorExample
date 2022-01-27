using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectTest
{
    [Injection(typeof(GlobalAspect),Propagation = PropagateTo.Methods)]
    public interface ISomeService
    {
        [Validation]
        [Log]
        [Cache]
        void Hello();
    }
    
    public class SomeService : ISomeService
    {     
        public void Hello()
        {
            Console.WriteLine("Orjinal Metod içi: Merhaba Dünya");
        }
    }
}
