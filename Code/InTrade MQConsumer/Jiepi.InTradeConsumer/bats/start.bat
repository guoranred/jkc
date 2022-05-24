::Powershell.exe -executionpolicy remotesigned -File %~dp0start.ps1
sc start JiepiInTradeConsumer
pause