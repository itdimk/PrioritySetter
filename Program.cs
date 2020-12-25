using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using PrioritySetter.BusinessLogic;

namespace PrioritySetter
{
    class Program
    {
        static readonly CpuPrioritySetter _setter = new CpuPrioritySetter();

        static void Main(string[] args)
        {
            string appName = GetAppName();
            PrintCurrentPriority(appName);
            var priority = GetCpuPriority();

            if (_setter.SetPriority(appName, priority))
                Console.WriteLine("Success!");
            else
                Console.WriteLine("Something went wrong");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void PrintCurrentPriority(string appName)
        {
            try
            {
                var priority = _setter.GetPriority(appName);
                Console.WriteLine($"Priority of {appName} is {priority.ToString()}");
            }
            catch (Exception e) 
            {
                Console.WriteLine($"{appName} has no priority setting in registry");
            }
        }

        static ICpuPrioritySetter.CpuPriority GetCpuPriority()
        {
            string[] priorities = {"idle", "normal", "high", "realtime", "below normal", "above normal"};

            string priority;

            do
            {
                Console.WriteLine("Type priority ");
                priority = Console.ReadLine();
            } while (!priorities.Contains(priority));

            return (ICpuPrioritySetter.CpuPriority) Array.IndexOf(priorities, priority) + 1;
        }

        static string GetAppName()
        {
            string appName;

            do
            {
                Console.WriteLine("Type app name (like app.exe): ");
                appName = Console.ReadLine();
            } while (!Regex.IsMatch(appName ?? "", ".*.exe$"));

            return appName;
        }
    }
}