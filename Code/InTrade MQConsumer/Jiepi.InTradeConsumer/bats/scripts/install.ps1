$dir = get-location
$fileName = "Jiepi.InTradeConsumer.Service.exe"
$filePath ="$dir\$fileName"
New-Service -Name "JiepiInTradeConsumer" -BinaryPathName $filePath -Description "ERP��ó�����߷���" -DisplayName "JiepiInTradeConsumer" -StartupType Automatic