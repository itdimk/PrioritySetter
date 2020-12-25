using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using PrioritySetter.BusinessLogic;
using PrioritySetter.Utils;

namespace PrioritySetter
{
    class Program
    {
        static readonly ICpuPrioritySetter _setter = new CpuPrioritySetter();

        static void Main(string[] args)
        {
            string appName = ReadAppName(args);
            CpuPriority oldPriority = _setter.GetPriority(appName);
            WriteCpuPriority(appName, oldPriority);
            
            CpuPriority newPriority = ReadCpuPriority(args);
            _setter.SetPriority(appName, newPriority);

            Console.WriteLine("Success!");
            DelayedExit(1);
        }

        static void WriteCpuPriority(string appName, CpuPriority priority)
        {
            Console.WriteLine($"Current priority of {appName} is {priority.ToString()}");
        }

        static CpuPriority ReadCpuPriority(string[] args)
        {
            string[] priorities = {"idle", "normal", "high", "realtime", "below normal", "above normal"};

            string priority = args.Length > 1 ? args[1] : ConsoleEx.ReadString("Type new priority:");

            while (!priorities.Contains(priority))
            {
                Console.WriteLine("Available priorities:");
                ConsoleEx.WriteLine(priorities);
                priority = ConsoleEx.ReadString("Type one of them:");
            }

            return (CpuPriority) Array.IndexOf(priorities, priority) + 1;
        }

        static string ReadAppName(string[] args)
        {
            string appName = args.Length > 0 ? args[0] : ConsoleEx.ReadString("Type app name (like app.exe):");

            while (!Regex.IsMatch(appName ?? "", ".*.exe$"))
                appName = ConsoleEx.ReadString("Wrong app name. It should be like \"app.exe\"");

            return appName;
        }

        static void DelayedExit(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}