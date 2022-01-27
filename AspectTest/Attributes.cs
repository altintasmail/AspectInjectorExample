using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectTest
{
    //[Injection(typeof(LogAspect))]
    public class Log : AttributeBase
    {
        public override void RunBefore(string methodName)
        {
            Console.WriteLine($"metod:{methodName} Log Before");
        }
        public override void RunAfter(string methodName)
        {
            Console.WriteLine($"metod:{methodName} Log After");
        }
    }

    //[Injection(typeof(CacheAspect))]
    public class Cache : AttributeBase
    {
        public override void RunBefore(string methodName)
        {

            Console.WriteLine($"metod:{methodName} Cache Before");
        }
        public override void RunAfter(string methodName)
        {
            Console.WriteLine($"metod:{methodName} Cache After");
        }
    }

    //[Injection(typeof(ValidationAspect))]
    public class Validation : AttributeBase
    {
        public override void RunBefore(string methodName) {
            Console.WriteLine($"metod:{methodName} Validation Before");
        }
        public override void RunAfter(string methodName)
        {
            Console.WriteLine($"metod:{methodName} Validation After");
        }
    }

    public class AttributeBase : Attribute
    {
        public virtual void RunBefore(string methodName) { }
        public virtual void RunAfter(string methodName) { }
    }
}
