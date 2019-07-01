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
            Console.WriteLine($"({HashCode}) {message}");
            return $"Hi ({HashCode}) {message}";
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
            IServiceProvider serviceProvider2;
            IServiceProvider serviceProvider3;
            IServiceScope serviceScope2;
            IServiceScope serviceScope3;
            ISayHello sayHello1;
            ISayHello sayHello2;
            ISayHello sayHello3;
            ISayHello sayHello1_1;
            ISayHello sayHello2_1;
            ISayHello sayHello3_1;

            serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ISayHello, SayHello>();
            serviceProvider1 = serviceCollection.BuildServiceProvider();

            #region 使用單一 Singleton 
            serviceScope2 = serviceProvider1.CreateScope();
            serviceProvider2 = serviceScope2.ServiceProvider;
            sayHello1 = serviceProvider2.GetService<ISayHello>();
            sayHello1.Hi("M1 - Vulcan");
            sayHello2 = serviceProvider2.GetService<ISayHello>();
            sayHello2.Hi("M2 - Lee");
            sayHello1 = null;
            sayHello2 = null;
            GC.Collect(2);
            Thread.Sleep(1000);
            sayHello3 = serviceProvider2.GetService<ISayHello>();
            sayHello3.Hi("M9 - Vulcan Lee");

            serviceScope3 = serviceProvider1.CreateScope();
            serviceProvider3 = serviceScope3.ServiceProvider;
            sayHello1_1 = serviceProvider3.GetService<ISayHello>();
            sayHello1_1.Hi("M1_1 - Ada");
            sayHello2_1 = serviceProvider3.GetService<ISayHello>();
            sayHello2_1.Hi("M2_1 - Chan");
            sayHello1_1 = null;
            sayHello2_1 = null;
            GC.Collect(2);
            Thread.Sleep(1000);
            sayHello3_1 = serviceProvider3.GetService<ISayHello>();
            sayHello3_1.Hi("M3_1 - Ada Chan");
            // 若將底下的程式碼註解起來(在 AddScoped 模式)，則 
            // sayHello1, sayHello2 指向到 ConsoleMessage 會被釋放掉
            sayHello3.Hi("M3 - Vulcan Lee");
            #endregion
            //Console.WriteLine("Press any key for continuing...");
            //Console.ReadKey();
        }
    }
}
