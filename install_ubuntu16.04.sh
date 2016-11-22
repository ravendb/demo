#!/bin/bash
 
if [[ $UID != 0 ]]; then
    echo "Please run this script with sudo:"
    echo "sudo $0 $*"
    exit 1
fi
 
sudo echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
sudo apt-get update
sudo apt-get install dotnet-dev-1.0.0-preview2.1-003177
 
wget https://github.com/PowerShell/PowerShell/releases/download/v6.0.0-alpha.12/powershell_6.0.0-alpha.12-1ubuntu1.16.04.1_amd64.deb
sudo dpkg -i powershell_6.0.0-alpha.12-1ubuntu1.16.04.1_amd64.deb
sudo apt-get install -f
rm powershell_6.0.0-alpha.12-1ubuntu1.16.04.1_amd64.deb