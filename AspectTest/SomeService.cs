using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectTest
{
    [Injection(typeof(ValidationAspect),Propagation = PropagateTo.Methods)]
    public interface ISomeService
    {
        [Validation]
        void Hello();
    }
    
    public class SomeService : ISomeService
    {
        [Log]
        [Cache]        
        public void Hello()
        {
            Console.WriteLine("Orjinal Metod içi: Merhaba Dünya");
        }
    }
}
