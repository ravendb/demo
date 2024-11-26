param([switch]$NoBuild = $false)

if (!$NoBuild) {
    docker build -t ravendb/demo .
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed"
    }
}

$alreadyRunning = $(docker ps | select-string DemoServer)
if ($alreadyRunning) {
    throw "Demo server image is already running."
}

Start-Process -FilePath 'cmd.exe' -ArgumentList '/c docker run -e ASPNETCORE_URLS=http://*:8080 --rm -p 8080:8080 ravendb/demo'
Start-Sleep -Seconds 4

Start-Process http://localhost:8080
