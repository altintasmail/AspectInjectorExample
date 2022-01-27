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
        private List<AttributeBase> Attributes { get; set; }

        [Advice(Kind.Around)]
        public override object Execute([Argument(Source.Target)] Func<object[], object> methodDelegate, [Argument(Source.Arguments)] object[] args, [Argument(Source.Name)] string name)
        {
            Console.WriteLine($"metod:{name} içi");

            if (Attributes == null || Attributes.Count == 0)
                return methodDelegate(args);

            object result = null;

            //1. yol
            //Icache den baslayarak execute eder.
            //cache dersa dönen veri null değildir.
            //eğer null dönerse metod uygulanır. ve diğer attributlerde null olmayacağı için çalışmaz.
            Attributes.OrderByDescending(p => typeof(ICache).IsAssignableFrom(p.GetType())).ToList().ForEach(p => result = p.Execute(methodDelegate, args, name, result));

            //2. yol 
            //Attributes.Where(p => typeof(ICache).IsAssignableFrom(p.GetType())).Select(p => result = p.Execute(methodDelegate, args, name, result));
            //if(result==null){return methodDelegate(args);}

            return result;
        }

        [Advice(Kind.Before)]
        public override void OnBefore([Argument(Source.Name)] string name, [Argument(Source.Metadata)] MethodBase methodBase, [Argument(Source.Type)] Type type)
        {
            Attributes = ReflectionUtils.GetAttributes<AttributeBase>(name, methodBase, type);

            Attributes.ForEach(p => p.RunBefore(name));
        }

        [Advice(Kind.After)]
        public override void OnAfter([Argument(Source.Name)] string name)
        {
            Attributes.ForEach(p => p.RunAfter(name));
        }
    }

    public abstract class AspectBase
    {
        public abstract void OnBefore(string name, MethodBase methodBase, Type type);
        public abstract void OnAfter(string name);
        public abstract object Execute(Func<object[], object> methodDelegate, object[] args, string name);
    }
}
