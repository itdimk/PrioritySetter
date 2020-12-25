using System;
using System.Linq;
using System.Security.AccessControl;
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

        public bool SetPriority(string appName, ICpuPrioritySetter.CpuPriority cpuPriority)
        {
            
            var imageExecOptions = Root.OpenSubKey(ImageFileExecOptions,  true);

            if (imageExecOptions == null)
                throw new Exception($"Can't open subkey: {ImageFileExecOptions}");

            CreateSubkeysIfRequired(imageExecOptions, appName);

            var subkey = Root.OpenSubKey(GetPerfOptionsPath(appName),true);
            
            if(subkey == null)
                throw new Exception($"Can't open subkey {GetPerfOptionsPath(appName)}");
            
            subkey.SetValue(CpuPriorityClass, (int) cpuPriority, RegistryValueKind.DWord);

            return true;
        }

        public ICpuPrioritySetter.CpuPriority GetPriority(string appName)
        {
            string path = GetPerfOptionsPath(appName);
            var subkey = Root.OpenSubKey(path);

            if (subkey == null)
                throw new Exception($"Can't open subkey: {path}");

            int? priority = (int?) subkey.GetValue(CpuPriorityClass);

            if (!priority.HasValue)
                throw new Exception($"Can't get value: {path}\\{CpuPriorityClass}");

            return (ICpuPrioritySetter.CpuPriority) priority.Value;
        }


        private void CreateSubkeysIfRequired(RegistryKey imageExecOptions, string appName)
        {
            if (imageExecOptions == null)
                throw new NullReferenceException(nameof(imageExecOptions));

            bool createAppSubkey = !imageExecOptions.GetSubKeyNames().Contains(appName);

            if (createAppSubkey && imageExecOptions.CreateSubKey(appName, true) == null)
                throw new Exception($"Can't create {appName} subkey in {ImageFileExecOptions}");

            var appSubkey = imageExecOptions.OpenSubKey(appName, true);

            if (appSubkey == null)
                throw new Exception($"Can't open {appName} in {ImageFileExecOptions}");

            bool createPerfOptions = !appSubkey.GetSubKeyNames().Contains(PerfOptions);

            if (createPerfOptions)
                appSubkey.CreateSubKey(PerfOptions, true);
        }

        private string GetPerfOptionsPath(string appName)
        {
            return $"{ImageFileExecOptions}\\{appName}\\{PerfOptions}";
        }
    }
}

// HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\chrome.exe\PerfOptions
// "CpuPriorityClass"=dword:00000006
// 1 Idle
// 2 Normal
// 3 High
// 4 RealTime (n.a.)
// 5 Below Normal
// 6 Above Normal