Hi everyone!
I've created simple app to set cpu priority for specified app via windows registry.
It modifies "CpuPriorityClass" in "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\appname.exe\PerfOptions"

IMPORTANT STUFF:
- you have to run app with Administrator rights only
- you can type one of these priorities: "idle", "normal", "high", "realtime", "below normal", "above normal"

