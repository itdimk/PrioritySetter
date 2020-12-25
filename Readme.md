<h3>Hi everyone!</h3>
I've created simple app to set cpu priority for specified app via windows registry.
<br>
It modifies "CpuPriorityClass" in <small>HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\appname.exe\PerfOptions</small>

<h5>IMPORTANT STUFF</h5>
- You need Administrator's rights to use this app
- You can type one of these priorities: "idle", "normal", "high", "realtime", "below normal", "above normal"
- You can pass app name and priority as command line arguments