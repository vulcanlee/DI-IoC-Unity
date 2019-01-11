﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace LifetimeManager
{
    public interface IMessage
    {
        void Write(string message);
    }

    public class ConsoleMessage : IMessage
    {
        public void Write(string message)
        {
            Console.WriteLine($"此物件的 HashCode {this.GetHashCode()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 這裡將會建立 DI 容器
            IUnityContainer container = new UnityContainer();

            // 進行抽象型別與具體實作類別的註冊
            container.RegisterType<IMessage, ConsoleMessage>(
                "TransientLifetimeManager", new TransientLifetimeManager());
            container.RegisterType<IMessage, ConsoleMessage>(
                "ContainerControlledLifetimeManager", new ContainerControlledLifetimeManager());

            Console.WriteLine("TransientLifetimeManager解析就產生新物件、容器不會保留該物件參考");

            // 進行抽象型別的具體實作物件的解析
            IMessage messageTransientLifetimeManager1 = 
                container.Resolve<IMessage>("TransientLifetimeManager");
            IMessage messageTransientLifetimeManager2 = 
                container.Resolve<IMessage>("TransientLifetimeManager");

            // 執行取得物件的方法
            messageTransientLifetimeManager1.Write("Hi TransientLifetimeManager 1");
            messageTransientLifetimeManager2.Write("Hi TransientLifetimeManager 2");

            Console.WriteLine("ContainerControlledLifetimeManager類似 Singleton，解析時會得到相同物件，容器物件回收，此物件也回收");

            IMessage messageContainerControlledLifetimeManager1 = 
                container.Resolve<IMessage>("ContainerControlledLifetimeManager");
            IMessage messageContainerControlledLifetimeManager2 = 
                container.Resolve<IMessage>("ContainerControlledLifetimeManager");
            messageContainerControlledLifetimeManager1.Write("Hi ContainerControlledLifetimeManager 1");
            messageContainerControlledLifetimeManager2.Write("Hi ContainerControlledLifetimeManager 2");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
