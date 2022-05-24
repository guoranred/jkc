$dir = get-location
$fileName = "Jiepei.ErpConsumer.Service.exe"
$filePath ="$dir\publish\$fileName"
New-Service -Name "JiepeiErpConsumer" -BinaryPathName $filePath -Description "ERP消费者服务" -DisplayName "JiepeiErpConsumer" -StartupType Automatic