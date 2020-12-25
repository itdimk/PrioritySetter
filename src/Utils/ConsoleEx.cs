using System;
using System.Collections.Generic;
using System.Text;

namespace PrioritySetter.Utils
{
    public static class ConsoleEx
    {
        public static string ReadString(string message)
        {
            Console.Write(message + " ");
            return Console.ReadLine();
        }

        public static void WriteLine<T>(IEnumerable<T> items, string separator = ", ")
        {
            var builder = new StringBuilder();

            foreach (var item in items)
            {
                builder.Append(item);
                builder.Append(separator);
            }

            string text = builder.ToString().PadRight(separator.Length);
            Console.WriteLine(text);
        }
    }
}