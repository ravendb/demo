$ErrorActionPreference = 'Stop'

docker build -t ravendb/demo .
if ($LASTEXITCODE -ne 0) {
    throw "Build failed"
}

docker login -u ravendb
if ($LASTEXITCODE -ne 0) {
    throw "Login to docker hub failed."
}

docker push ravendb/demo 

docker logout
