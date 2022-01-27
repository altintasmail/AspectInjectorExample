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
    public class Cache : AttributeBase, ICache
    {
        public override void RunBefore(string methodName)
        {

            Console.WriteLine($"metod:{methodName} Cache Before");
        }
        public override void RunAfter(string methodName)
        {
            Console.WriteLine($"metod:{methodName} Cache After");
        }

        public override object Execute(Func<object[], object> methodDelegate, object[] args, string name, object val)
        {
            var cached = true;//iscached ile kontrol edilebilir.

            if (cached)
                return GetCachedVal(name);

            var result = methodDelegate(args);
            SetCache(name, result);

            return result;
        }

        public object GetCachedVal(string key)
        {
            //cache okuma işlemleri
            return "Cache edilmiş veri.";
        }

        public void SetCache(string key, object val)
        {
            //cache kaydetme işlemleri
        }
    }

    //[Injection(typeof(ValidationAspect))]
    public class Validation : AttributeBase
    {
        public override void RunBefore(string methodName)
        {
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
        public virtual object Execute(Func<object[], object> methodDelegate, object[] args, string name, object val)
        {
            if (val != null)
                return val;

            return methodDelegate(args);
        }
    }

    public interface ICache
    { }
}
