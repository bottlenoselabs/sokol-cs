#!/bin/bash

cmake -S ./src/c/sokol -B ./cmake-build-release -G 'Unix Makefiles' -DCMAKE_BUILD_TYPE=Release
make -C ./cmake-build-release
rm -r ./cmake-build-release
