#!/bin/bash

if [[ -n "$IS_WSL" || -n "$WSL_DISTRO_NAME" ]]; then
    TOOLCHAIN="-DCMAKE_BUILD_TYPE=Release -DCMAKE_TOOLCHAIN_FILE=$(pwd)/ext/c2cs/src/c/mingw-w64-x86_64.cmake"
else
    TOOLCHAIN=""
fi

cmake -S ./src/c/sokol -B ./cmake-build-release -G 'Unix Makefiles' -DCMAKE_BUILD_TYPE=Release $TOOLCHAIN
make -C ./cmake-build-release
rm -r ./cmake-build-release