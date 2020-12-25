using System;
using System.Linq;
using Microsoft.Win32;

namespace PrioritySetter.BusinessLogic
{
    public class CpuPrioritySetter : ICpuPrioritySetter
    {
        private readonly RegistryKey Root = Registry.LocalMachine;

        private const string ImageFileExecOptions =
            @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options";

        private const string CpuPriorityClass = "CpuPriorityClass";
        private const string PerfOptions = "PerfOptions";

        public void SetPriority(string appName, CpuPriority cpuPriority)
        {
            var perfOptsSubkey = OpenOrCreatePerfOptionsSubkey(appName, true);
            perfOptsSubkey.SetValue(CpuPriorityClass, (int) cpuPriority);
        }

        public CpuPriority GetPriority(string appName)
        {
            var perfOptsSubkey = OpenOrCreatePerfOptionsSubkey(appName, false);
            int? cpuPriority = (int?) perfOptsSubkey.GetValue(CpuPriorityClass);

            if (cpuPriority.HasValue)
                return (CpuPriority) cpuPriority.Value;
            else
                return CpuPriority.Normal;
        }


        private RegistryKey OpenOrCreateAppSubkey(string appName, bool writable)
        {
            return OpenOrCreateSubkey(ImageFileExecOptions, appName, writable);
        }

        private RegistryKey OpenOrCreatePerfOptionsSubkey(string appName, bool writable)
        {
            var appSubkey = OpenOrCreateAppSubkey(appName, true);
            return OpenOrCreateSubkey(appSubkey, PerfOptions, writable);
        }

        private RegistryKey OpenOrCreateSubkey(string keyPath, string subkeyName, bool writable)
        {
            var key = Root.OpenSubKey(keyPath, true);
            return OpenOrCreateSubkey(key, subkeyName, writable);
        }

        private RegistryKey OpenOrCreateSubkey(RegistryKey writeableKey, string subkeyName, bool writable)
        {
            bool createSubkey = !writeableKey.GetSubKeyNames().Contains(subkeyName);

            return createSubkey
                ? writeableKey.CreateSubKey(subkeyName, writable)
                : writeableKey.OpenSubKey(subkeyName, writable);
        }
    }
}