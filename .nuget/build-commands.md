dotnet build .\VL.Avalonia\src\VL.Avalonia.csproj --configuration Release --output .\VL.Avalonia\lib\net8.0\
nuget pack .nuget\VL.Avalonia.nuspec -Version 0.0.1-pre -OutputDirectory .\packages

dotnet build .\VL.Avalonia.Skia\src\VL.Avalonia.Skia.csproj --configuration Release --output .\VL.Avalonia.Skia\lib\net8.0\
nuget pack .nuget\VL.Avalonia.Skia.nuspec -Version 0.0.1-pre -OutputDirectory .\packages

dotnet build .\VL.Avalonia.Custom\src\VL.Avalonia.Custom.csproj --configuration Release --output .\VL.Avalonia.Custom\lib\net8.0\
nuget pack .nuget\VL.Avalonia.Custom.nuspec -Version 0.0.1-pre -OutputDirectory .\packages

nuget pack .nuget\VL.Avalonia.Stride.nuspec -Version 0.0.1-pre -OutputDirectory .\packages


https://stackoverflow.com/questions/34442421/nuget-pack-how-to-set-change-dependency-version-via-command-line