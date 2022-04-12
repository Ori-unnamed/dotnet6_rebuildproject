using System.Linq.Expressions;
using System.Reflection;



namespace WebApplication1
{
    [AttributeUsage(AttributeTargets.Method)]
    public class WebRouter:Attribute
    {
        public string path;
        public HttpMethod method;
        public WebRouter(string path)
        {
            this.path = path;
            this.method = HttpMethod.Post;
        }
        public WebRouter(string path, HttpMethod method)
        {
            this.path=path;
            this.method = method; 
            
        }
        
    }

    internal class ExpressionTool
    {
        internal static Func<TObj, Tin, Tout> CreateMethodDelegate<TObj, Tin, Tout>(MethodInfo method)
        {
            var mParameter = Expression.Parameter(typeof(TObj), "m");
            var pParameter = Expression.Parameter(typeof(Tin), "p");
            var mcExpression = Expression.Call(mParameter, method, Expression.Convert(pParameter, typeof(Tin)));
            var reExpression = Expression.Convert(mcExpression, typeof(Tout));
            return Expression.Lambda<Func<TObj, Tin, Tout>>(reExpression, mParameter, pParameter).Compile();
        }
        internal static Func<TObj, Tout> CreateMethodDelegate<TObj, Tout>(MethodInfo method)
        {
            var mParameter = Expression.Parameter(typeof(TObj), "m");
            var mcExpression = Expression.Call(mParameter, method);
            var reExpression = Expression.Convert(mcExpression, typeof(Tout));
            return Expression.Lambda<Func<TObj, Tout>>(reExpression, mParameter).Compile();
        }
    }
}
