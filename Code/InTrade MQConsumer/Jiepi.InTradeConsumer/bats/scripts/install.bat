cd /d %~dp0
Powershell.exe -executionpolicy remotesigned -File %~dp0install.ps1
pause