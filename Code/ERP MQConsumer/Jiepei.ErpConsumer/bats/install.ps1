$dir = get-location
$fileName = "Jiepei.ErpConsumer.Service.exe"
$filePath ="$dir\publish\$fileName"
New-Service -Name "JiepeiErpConsumer" -BinaryPathName $filePath -Description "ERP�����߷���" -DisplayName "JiepeiErpConsumer" -StartupType Automatic