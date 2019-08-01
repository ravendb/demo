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

Start-Process -FilePath 'cmd.exe' -ArgumentList '/c docker run --rm -p 8081:80 ravendb/demo'
Start-Sleep -Seconds 4

Start-Process http://localhost:8081
