﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DeepResolve
{
    interface IDependency1 { }
    class Dependency1 : IDependency1 { }
    interface IDependency2 { }
    class Dependency2 : IDependency2 { }
    interface IDependency3 { }
    class Dependency3 : IDependency3 { }
    interface IDependency4 { }
    class Dependency4 : IDependency4 { }

    interface IInfrastructure
    {
        string Name { get; set; }
    }

    class Infrastructure : IInfrastructure
    {
        public string Name { get; set; }
        IDependency1 D1;
        IDependency2 D2;
        IDependency3 D3;
        IDependency4 D4;

        public Infrastructure(IDependency1 d1,
            IDependency2 d2,
            IDependency3 d3,
            IDependency4 d4)
        {
            D1 = d1;
            D2 = d2;
            D3 = d3;
            D4 = d4;
            Console.WriteLine($"Dependency1 : {D1.GetHashCode()}");
            Console.WriteLine($"Dependency2 : {D2.GetHashCode()}");
            Console.WriteLine($"Dependency3 : {D3.GetHashCode()}");
            Console.WriteLine($"Dependency4 : {D4.GetHashCode()}");
        }
    }

    class ViewModel
    {
        private readonly IInfrastructure _Infrastructure;

        public ViewModel(IInfrastructure infrastructure)
        {
            this._Infrastructure = infrastructure;
            Console.WriteLine($"Infrastructure : {_Infrastructure.GetHashCode()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 這裡將會建立 DI 容器
            IUnityContainer container = new UnityContainer();

            // 進行抽象型別與具體實作類別的註冊
            container.RegisterType<IDependency1, Dependency1>();
            container.RegisterType<IDependency2, Dependency2>();
            container.RegisterType<IDependency3, Dependency3>();
            container.RegisterType<IDependency4, Dependency4>();
            container.RegisterType<IInfrastructure, Infrastructure>();

            // 進行抽象型別的具體實作物件的解析
            var foo = container.Resolve<ViewModel>();

            // 執行取得物件的方法
            Console.WriteLine($"ViewModel : {foo.GetHashCode()}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
