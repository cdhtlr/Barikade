# Barikade
![](https://raw.githubusercontent.com/cdhtlr/Barikade/main/Barikade/logo..png "Barikadde Logo")

Barikade (in Indonesian) or barricade (in English) is a Windows Service to close background and foreground processes running outside of permitted directories.

It's a sort of allowlist but not an app blocker. Barikade will try to close the process and prevent inbound and outbound network connections if it detects a running process that comes from outside the permitted directories.

It can prevent the use of legit software to bypass Windows Firewall and download malware by only allowing applications within AllowedPaths.ini to run and access the network.

You should not use Barikade as the main protection layer for Windows hardening. Use an antivirus, firewall, access control, applocker, etc. to configure Windows security until it is completely safe, then add Barikade if needed.

Barikade uses WinDivert to work, there may be a failure to prevent network connection. So, make sure you use it with caution.
