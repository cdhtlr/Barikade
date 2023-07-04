# Barikade
![](https://raw.githubusercontent.com/cdhtlr/Barikade/main/logo.png "Barikadde Logo")

Barikade (in Indonesian) or barricade (in English) is a Windows Service to close background and foreground processes running outside of permitted directories.

It's a sort of allowlist but not an app blocker. Barikade will try to close the process and prevent inbound and outbound network connections if it detects a running process that comes from outside the permitted directories.

It can prevent the use of legit software to bypass Windows Firewall and download malware by only allowing applications within AllowedPaths.ini to run and access the network.

You should not use Barikade as the main protection layer for Windows hardening. Use an antivirus, firewall, access control, applocker, etc. to configure Windows security until it is completely safe, then add Barikade if needed.

Barikade uses WinDivert to work, there may be a failure to prevent network connection. So, make sure you use it with caution.

Click this YouTube thumbnail for demo:

[![Video](https://img.youtube.com/vi/oyM5S3AnAnU/hqdefault.jpg)](https://www.youtube.com/watch?v=oyM5S3AnAnU)

## How to use
Because Barikade is a Windows Service, to run it please open a command prompt or terminal as an administrator then run the command below:
```
sc create "Barikade" binpath= C:\example\path\of\Barikade.exe start= auto
```

Then run Barikade for the first time.
```
sc start Barikade
```

AllowedPaths.ini file will be created in the directory where Barikade.exe is located, edit it and fill it with the directory containing the programs that are allowed to run.

### Example
```
+C:\Program Files\
+C:\Program Files (x86)\
+C:\Windows\
-D:\Portable Program\
```

> Directories that start with a + sign are recursive, while directories that start with a - sign are non-recursive.
> Barikade must be in one of the listed directories, otherwise it will not be able to run.

### Logging
```
Create a Log.txt file in the Barikade program directory to enable file deletion logging
```

Restart Barikade service or restart Windows to apply changes.
