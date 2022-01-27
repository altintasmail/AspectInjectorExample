using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectTest
{
    [Injection(typeof(LogAspect))]
    class Log : Attribute
    {
    }

    [Injection(typeof(CacheAspect))]
    class Cache : Attribute
    {
    }

    [Injection(typeof(ValidationAspect))]
    class Validation : Attribute
    {
    }
}
