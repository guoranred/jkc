$dir = get-location
$fileName = "Jiepi.InTradeConsumer.Service.exe"
$filePath ="$dir\$fileName"
New-Service -Name "JiepiInTradeConsumer" -BinaryPathName $filePath -Description "ERP内贸消费者服务" -DisplayName "JiepiInTradeConsumer" -StartupType Automatic