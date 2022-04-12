using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace WebApplication1
{
    public interface IMyService
    {
        Instance = ([FromServices] IServiceProvider sp, Tinput input) => ExpressionTool.CreateMethodDelegate<Timpl, Tinput, Toutput>(method) (sp.GetService(typeof(Tsvc)) as Timpl, input);
    }

    public class MyService
    {
        IServiceProvider sp;
        void test()
        {
            sp.GetService(typeof());
        }
    }

    public abstract class DynamicPorxy
    {
        public abstract Delegate Instance { get; set; }
    }
    public class DynamicPorxyImpl<Tsvc, Timpl, Tinput, Toutput> : DynamicPorxy where Timpl : class where Tinput : class where Toutput : class
    {
        public override Delegate Instance { get; set; }
        public DynamicPorxyImpl(MethodInfo method)
        {
            Instance = ([FromServices] IServiceProvider sp, Tinput input) => ExpressionTool.CreateMethodDelegate<Timpl, Tinput, Toutput>(method)(sp.GetService(typeof(Tsvc)) as Timpl, input);
            DependencyContext.Default.CompileLibraries.Where(x => !x.Serviceable && x.Type != "package")
                .Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name))).SelectMany(
                x=>x.GetTypes().Where(x=>!x.IsInterface && x.GetInterfaces().Any()))
        }
    }
}
