#!/bin/bash

function exitIfLastCommandFailed() {
    error=$?
    if [ $error -ne 0 ]; then
        exit $error
    fi
}

function download_C2CS_ubuntu() {
    if [ ! -f "./C2CS" ]; then
        wget https://nightly.link/lithiumtoast/c2cs/workflows/build-test-deploy/develop/ubuntu.20.04-x64.zip
        unzip ./ubuntu.20.04-x64.zip
        rm ./ubuntu.20.04-x64.zip
        chmod +x ./C2CS
    fi
}

function download_C2CS_osx() {
    if [ ! -f "./C2CS" ]; then
        wget https://nightly.link/lithiumtoast/c2cs/workflows/build-test-deploy/develop/osx-x64.zip
        unzip ./osx-x64.zip
        rm ./osx-x64.zip
        chmod +x ./C2CS
    fi
}

function bindgen_sokol_app {
    ./C2CS ast -i ./ext/sokol/sokol_app.h -o ./sokol_app.json -s ./ext/sokol
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_app.json -o ./src/cs/production/sokol-cs/sokol_app.cs -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_app.json
    exitIfLastCommandFailed
}

function bindgen_sokol_gfx {
    ./C2CS ast -i ./ext/sokol/sokol_gfx.h -o ./sokol_gfx.json -s ./ext/sokol
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_gfx.json -o ./src/cs/production/sokol-cs/sokol_gfx.cs -a "sg_color->Rgba32F" -g Rgba32F -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_gfx.json
    exitIfLastCommandFailed
}

function bindgen_sokol_glue {
    ./C2CS ast -i ./ext/sokol/sokol_glue.h -o ./sokol_glue.json -s ./ext/sokol -a "--include=sokol_gfx.h" "--include=sokol_app.h" -g sokol_gfx.h sokol_app.h
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_glue.json -o ./src/cs/production/sokol-cs/sokol_glue.cs -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_glue.json
    exitIfLastCommandFailed
    # Dirty trick :)
    sed -i .bak -e "s/^using C2CS;/using C2CS;\nusing static sokol_gfx;/" ./src/cs/production/sokol-cs/sokol_glue.cs
    rm ./src/cs/production/sokol-cs/sokol_glue.cs.bak
}

function bindgen_sokol_audio {
    ./C2CS ast -i ./ext/sokol/sokol_audio.h -o ./sokol_audio.json -s ./ext/sokol
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_audio.json -o ./src/cs/production/sokol-cs/sokol_audio.cs -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_audio.json
    exitIfLastCommandFailed
}

function bindgen_sokol_fetch {
    ./C2CS ast -i ./ext/sokol/sokol_fetch.h -o ./sokol_fetch.json -s ./ext/sokol
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_fetch.json -o ./src/cs/production/sokol-cs/sokol_fetch.cs -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_fetch.json
    exitIfLastCommandFailed
}

function bindgen_sokol_time {
    ./C2CS ast -i ./ext/sokol/sokol_time.h -o ./sokol_time.json -s ./ext/sokol
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_time.json -o ./src/cs/production/sokol-cs/sokol_time.cs -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_time.json
    exitIfLastCommandFailed
}

function bindgen_sokol_args {
    ./C2CS ast -i ./ext/sokol/sokol_args.h -o ./sokol_args.json -s ./ext/sokol
    exitIfLastCommandFailed
    ./C2CS cs -i ./sokol_args.json -o ./src/cs/production/sokol-cs/sokol_args.cs -l "sokol"
    exitIfLastCommandFailed
    rm ./sokol_args.json
    exitIfLastCommandFailed
}

function bindgen() {
    bindgen_sokol_app
    bindgen_sokol_gfx
    bindgen_sokol_glue
    bindgen_sokol_audio
    bindgen_sokol_fetch
    bindgen_sokol_time
    bindgen_sokol_args
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
