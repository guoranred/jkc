$dir = get-location
$fileName = "Jiepi.InTradeConsumer.Service.exe"
$filePath ="$dir\publish\$fileName"
New-Service -Name "JiepiInTradeConsumer" -BinaryPathName $filePath -Description "ERP��ó�����߷���" -DisplayName "JiepiInTradeConsumer" -StartupType Automatic