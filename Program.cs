using System;
using System.Linq;
using System.Text.RegularExpressions;
using PrioritySetter.BusinessLogic;

namespace PrioritySetter
{
    class Program
    {
        static readonly CpuPrioritySetter _setter = new CpuPrioritySetter();

        static void Main(string[] args)
        {
            string appName = ReadAppName(args);
            CpuPriority oldPriority = _setter.GetPriority(appName);
            WriteCpuPriority(appName, oldPriority);
            
            CpuPriority newPriority = ReadCpuPriority(args);
            _setter.SetPriority(appName, newPriority);

            Console.WriteLine("Success! Press any key to exit");
            Console.ReadKey();
        }

        static void WriteCpuPriority(string appName, CpuPriority priority)
        {
            Console.WriteLine($"Priority of {appName} is {priority.ToString()}");
        }

        static CpuPriority ReadCpuPriority(string[] args)
        {
            string[] priorities = {"idle", "normal", "high", "realtime", "below normal", "above normal"};

            string priority = args.Length > 1 ? args[1] : ConsoleEx.ReadString("Type priority:");

            while (!priorities.Contains(priority))
            {
                Console.WriteLine("Available priorities:");
                ConsoleEx.WriteLine(priorities);
                priority = ConsoleEx.ReadString("Type priority:");
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
    }
}