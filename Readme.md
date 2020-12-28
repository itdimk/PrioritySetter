<h3>Hi everyone!</h3>
I've created simple app to set cpu priority for specified app PERMANENTLY (via windows registry).
<br>
It modifies "CpuPriorityClass" in <br/><i>HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\appname.exe\PerfOptions</i>

<h5>IMPORTANT STUFF</h5>

- You need Administrator rights to use this app
- If app you typed is running now, priority will be set at next launch (registry setting doesn't take effect instantly)
- You can type one of these priorities: "idle", "normal", "high", "realtime", "below normal", "above normal"
- You can integrate this app with cmd.exe (usage: set-priority mygame.exe high)
