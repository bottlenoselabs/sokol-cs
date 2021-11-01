#!/bin/bash

function exitIfLastCommandFailed() {
    error=$?
    if [ $error -ne 0 ]; then
        exit $error
    fi
}

function download_C2CS_ubuntu() {
    if [ ! -f "./C2CS" ]; then
        wget https://nightly.link/lithiumtoast/c2cs/workflows/build-test-deploy/main/ubuntu.20.04-x64.zip
        unzip ./ubuntu.20.04-x64.zip
        rm ./ubuntu.20.04-x64.zip
        chmod +x ./C2CS
    fi
}

function download_C2CS_osx() {
    if [ ! -f "./C2CS" ]; then
        wget https://nightly.link/lithiumtoast/c2cs/workflows/build-test-deploy/main/osx-x64.zip
        unzip ./osx-x64.zip
        rm ./osx-x64.zip
        chmod +x ./C2CS
    fi
}

function bindgen() {
    ./C2CS ast -i ./src/c/sokol/sokol.h -o ./sokol.json -s ./ext/sokol -b 64
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol.json -o ./src/cs/production/sokol-cs/sokol.cs -l "sokol" -a "sg_color->Rgba32F" -g ./ignored.txt
    exitIfLastCommandFailed
    rm ./sokol.json
    exitIfLastCommandFailed
}

unamestr="$(uname | tr '[:upper:]' '[:lower:]')"
if [[ "$unamestr" == "linux" ]]; then
    download_C2CS_ubuntu
elif [[ "$unamestr" == "darwin" ]]; then
    download_C2CS_osx
else
    echo "Unknown platform: '$unamestr'."
fi

bindgen
