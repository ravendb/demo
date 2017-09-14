dotnet restore
dotnet build "./DemoServer" -c Debug
cd "./DemoServer"
dotnet "bin/Debug/netcoreapp2.0/DemoServer.dll"
