using System;

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
            return $"Hi {message}";
        }
        ~SayHello()
        {
            Console.WriteLine($"SayHello ({HashCode}) 已經被釋放了");
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
