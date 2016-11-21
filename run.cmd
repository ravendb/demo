dotnet restore
dotnet build "./DemoServer" -c Debug
cd ./DemoServer/bin/Debug/netcoreapp1.1/
dotnet "DemoServer.dll"
