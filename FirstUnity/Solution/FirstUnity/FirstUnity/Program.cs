﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FirstUnity
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
            // 這裡將會建立 DI 容器
            IUnityContainer container = new UnityContainer();

            // 進行抽象型別與具體實作類別的註冊
            container.RegisterType<IMessage, ConsoleMessage>();

            // 請嘗試確認深層注入的行為，是否可以正常運作
            //container.RegisterType<IMessage, FileMessage>();
            //container.RegisterType<ILog, Log>();

            // 進行抽象型別的具體實作物件的解析
            IMessage message = container.Resolve<IMessage>();

            // 執行取得物件的方法
            message.Write("Hi Vulcan");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
