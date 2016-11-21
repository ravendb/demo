dotnet restore
dotnet build "./DemoServer" -c Debug
cd "./DemoServer"
dotnet "bin/Debug/netcoreapp1.1/DemoServer.dll"
