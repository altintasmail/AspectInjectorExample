using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AspectTest
{
    //[Aspect(Scope.Global)]
    //public class LogAspect : AspectBase
    //{
    //    [Advice(Kind.Before)]
    //    public override void OnBefore([Argument(Source.Name)] string name)
    //    {
    //        Console.WriteLine($"metod:{name} LogAspect öncesi");
    //    }

    //    [Advice(Kind.After)]
    //    public override void OnAfter([Argument(Source.Name)] string name)
    //    {
    //        Console.WriteLine($"metod:{name} LogAspect sonrası");
    //    }

    //    [Advice(Kind.Around)]
    //    public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate,[Argument(Source.Arguments)] object[] args)
    //    {
    //        Console.WriteLine("LogAspect içi");
    //        return methodDelegate(args);
    //    }
    //}

    //[Aspect(Scope.Global)]
    //public class CacheAspect : AspectBase
    //{
    //    [Advice(Kind.Before)]
    //    public override void OnBefore([Argument(Source.Name)] string name)
    //    {
    //        Console.WriteLine($"metod:{name} CacheAspect öncesi");
    //    }

    //    [Advice(Kind.After)]
    //    public override void OnAfter([Argument(Source.Name)] string name)
    //    {
    //        Console.WriteLine($"metod:{name} CacheAspect sonrası");
    //    }

    //    [Advice(Kind.Around)]
    //    public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate, [Argument(Source.Arguments)] object[] args)
    //    {
    //        Console.WriteLine("CacheAspect içi");
    //        return methodDelegate(args);
    //    }
    //}

    [Aspect(Scope.Global)]
    public class GlobalAspect : AspectBase
    {
        private List<Log> LogAttributes { get; set; }
        private List<Cache> CacheAttributes { get; set; }
        private List<Validation> ValidationAttributes { get; set; }
        //public GlobalAspect()
        //{
        //    LogAttributes = new List<Log>();
        //    CacheAttributes = new List<Cache>();
        //    ValidationAttributes = new List<Validation>();
        //}

        [Advice(Kind.Around)]
        public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate, [Argument(Source.Arguments)] object[] args, [Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} içi");
            return methodDelegate(args);
        }

        [Advice(Kind.Before)]
        public override void OnBefore([Argument(Source.Name)] string name, [Argument(Source.Metadata)] MethodBase methodBase, [Argument(Source.Type)] Type type)
        {
            LogAttributes = ReflectionUtils.GetAttributes<Log>(name, methodBase, type);
            CacheAttributes = ReflectionUtils.GetAttributes<Cache>(name, methodBase, type);
            ValidationAttributes = ReflectionUtils.GetAttributes<Validation>(name, methodBase, type);

            //Console.WriteLine($"metod:{name} öncesi");
            LogAttributes.ForEach(p=>p.RunBefore(name));
            CacheAttributes.ForEach(p=>p.RunBefore(name));
            ValidationAttributes.ForEach(p=>p.RunBefore(name));
        }

        [Advice(Kind.After)]
        public override void OnAfter([Argument(Source.Name)] string name)
        {
            //Console.WriteLine($"metod:{name} sonrası");
            LogAttributes.ForEach(p => p.RunAfter(name));
            CacheAttributes.ForEach(p => p.RunAfter(name));
            ValidationAttributes.ForEach(p => p.RunAfter(name));
        }
    }

    public abstract class AspectBase
    {
        public abstract void OnBefore(string name, MethodBase methodBase, Type type);
        public abstract void OnAfter(string name);
        public abstract object Execute(Func<object[], object> methodDelegate, object[] args, string name);
    }
}
