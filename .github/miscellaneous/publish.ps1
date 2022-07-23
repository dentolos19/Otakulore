Set-Location (Get-Item $PSScriptRoot).Parent.Parent.FullName

dotnet publish --framework $env:windowsTargetFramework --configuration Release -property:ApplicationVersion=$env:projectBuild
dotnet publish --framework $env:androidTargetFramework --configuration Release -property:ApplicationVersion=$env:projectBuild

$windowsBuild = (Get-ChildItem -Recurse -Path $env:projectName/bin/Release/net*-windows*/**/AppPackages/* -Filter *.msix -Exclude *WindowsAppRuntime*.msix).FullName
$androidBuild = (Get-ChildItem -Recurse -Path $env:projectName/bin/Release/net*-android -Filter *.apk).FullName

Compress-Archive -Path $windowsBuild -CompressionLevel Optimal -DestinationPath $env:windowsOutputFile
Compress-Archive -Path $androidBuild -CompressionLevel Optimal -DestinationPath $env:androidOutputFile