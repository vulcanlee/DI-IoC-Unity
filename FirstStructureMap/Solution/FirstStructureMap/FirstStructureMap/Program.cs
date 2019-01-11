﻿using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstStructureMap
{
    public interface IMessage
    {
        void Write(string message);
    }

    public class ConsoleMessage : IMessage
    {
        public void Write(string message)
        {
            Console.WriteLine($"訊息: {message} 已經寫入到螢幕上了");
        }
    }

    public class FileMessage : IMessage
    {
        ILog _Log;
        public FileMessage(ILog log)
        {
            _Log = log;
        }
        public void Write(string message)
        {
            Console.WriteLine($"訊息: {message} 已經寫入到檔案上了");
            _Log.Write(message);
        }
    }

    public interface ILog
    {
        void Write(string message);
    }

    public class Log : ILog
    {
        public void Write(string message)
        {
            Console.WriteLine($"訊息: {message} 已經寫入到 Log 上了");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 這裡可以在建立容器同時進行型別註冊
            //IContainer container = new Container(c =>
            //{
            //    c.For<IMessage>().Use<ConsoleMessage>();
            //});

            // 這裡將會建立 DI 容器
            IContainer container = new Container();

            // 進行抽象型別與具體實作類別的註冊
            container.Configure(config =>
            {
                config.For<IMessage>().Use<ConsoleMessage>();

                // 請嘗試確認深層注入的行為，是否可以正常運作
                //config.For<IMessage>().Use<FileMessage>();
                //config.For<ILog>().Use<Log>();
            });

            // 進行抽象型別的具體實作物件的解析
            IMessage message = container.GetInstance<IMessage>();

            // 執行取得物件的方法
            message.Write("Hi Vulcan");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
