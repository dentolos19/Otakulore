$projectName = "Otakulore"

Set-Location (Get-Item $PSScriptRoot).Parent.Parent.FullName

dotnet publish --framework net6.0-windows10.0.19041.0 --configuration Release --output build/windows
dotnet publish --framework net6.0-android --configuration Release --output build/android

$windowsBuild = (Get-ChildItem -Recurse -Path $projectName/bin/Release/net*-windows*/**/AppPackages/* -Filter *.msix -Exclude *WindowsAppRuntime*.msix).FullName
$androidBuild = (Get-ChildItem -Recurse -Path build/android -Filter *.apk).FullName

Compress-Archive -Path $windowsBuild -DestinationPath $projectName-windows.zip
Compress-Archive -Path $androidBuild -DestinationPath $projectName-android.zip