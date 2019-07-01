using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace NETCoreDI
{
    public interface ISayHello
    {
        string Hi(string message);
    }
    public class SayHello : ISayHello
    {
        int HashCode;
        public SayHello()
        {
            HashCode = this.GetHashCode();
            Console.WriteLine($"SayHello ({HashCode}) 已經被建立了");
        }
        public string Hi(string message)
        {
            Console.WriteLine(message);
            return $"Hi {message}";
        }
        ~SayHello()
        {
            Console.WriteLine($"SayHello ({HashCode}) 已經被釋放了");
        }
    }
    public class Program
    {
        static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection;
            IServiceProvider serviceProvider1;
            ISayHello sayHello1;
            ISayHello sayHello2;
            ISayHello sayHello3;

            serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ISayHello, SayHello>();
            serviceProvider1 = serviceCollection.BuildServiceProvider();

            #region 暫時性 Transient
            sayHello1 = serviceProvider1.GetService<ISayHello>();
            sayHello1.Hi("M1 - Vulcan");
            sayHello2 = serviceProvider1.GetService<ISayHello>();
            sayHello2.Hi("M2 - Lee");
            sayHello3 = serviceProvider1.GetService<ISayHello>();
            sayHello1 = null;
            sayHello2 = null;
            GC.Collect(2);
            Thread.Sleep(1000);
            sayHello3.Hi("M3 - Vulcan Lee");
            #endregion

            //Console.WriteLine("Press any key for continuing...");
            //Console.ReadKey();
        }
    }
}
