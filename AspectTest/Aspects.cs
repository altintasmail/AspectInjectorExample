using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectTest
{
    [Aspect(Scope.Global)]
    public class LogAspect : AspectBase
    {
        [Advice(Kind.Before)]
        public override void OnBefore([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} LogAspect öncesi");
        }

        [Advice(Kind.After)]
        public override void OnAfter([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} LogAspect sonrası");
        }

        [Advice(Kind.Around)]
        public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate,[Argument(Source.Arguments)] object[] args)
        {
            Console.WriteLine("LogAspect içi");
            return methodDelegate(args);
        }
    }

    [Aspect(Scope.Global)]
    public class CacheAspect : AspectBase
    {
        [Advice(Kind.Before)]
        public override void OnBefore([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} CacheAspect öncesi");
        }

        [Advice(Kind.After)]
        public override void OnAfter([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} CacheAspect sonrası");
        }

        [Advice(Kind.Around)]
        public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate, [Argument(Source.Arguments)] object[] args)
        {
            Console.WriteLine("CacheAspect içi");
            return methodDelegate(args);
        }
    }

    [Aspect(Scope.Global)]
    public class ValidationAspect : AspectBase
    {
        [Advice(Kind.Before)]
        public override void OnBefore([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} ValidationAspect öncesi");
        }

        [Advice(Kind.After)]
        public override void OnAfter([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} ValidationAspect sonrası");
        }

        [Advice(Kind.Around)]
        public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate, [Argument(Source.Arguments)] object[] args)
        {
            Console.WriteLine("ValidationAspect içi");
            return methodDelegate(args);
        }
    }

    public abstract class AspectBase
    {
        public abstract void OnBefore(string name);
        public abstract void OnAfter(string name);
        public abstract object Execute(Func<object[], object> methodDelegate, object[] args);
    }
}
