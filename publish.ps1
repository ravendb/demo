$ErrorActionPreference = 'Stop'

docker build -t ravendb/demo .
if ($LASTEXITCODE -ne 0) {
    throw "Build failed"
}

docker login -u ravendb

docker push ravendb/demo 

docker logout
