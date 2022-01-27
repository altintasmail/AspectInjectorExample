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
        string Hello();
    }
    
    public class SomeService : ISomeService
    {     
        public string Hello()
        {
            return "Orjinal Metod içi: Merhaba Dünya";
        }
    }
}
