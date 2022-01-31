#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
echo "Started building native libraries... Directory: $DIR"
LIB_DIR="$DIR/lib"
mkdir -p $LIB_DIR
echo "Started '$0' $1 $2 $3 $4"

if [[ ! -z "$1" ]]; then
    TARGET_BUILD_OS="$1"
fi

if [[ ! -z "$2" ]]; then
    TARGET_BUILD_ARCH="$2"
fi

function set_target_build_os {
    if [[ -z "$TARGET_BUILD_OS" || $TARGET_BUILD_OS == "host" ]]; then
        uname_str="$(uname -a)"
        case "${uname_str}" in
            *Microsoft*)    TARGET_BUILD_OS="windows";;
            *microsoft*)    TARGET_BUILD_OS="windows";;
            Linux*)         TARGET_BUILD_OS="linux";;
            Darwin*)        TARGET_BUILD_OS="macos";;
            CYGWIN*)        TARGET_BUILD_OS="linux";;
            MINGW*)         TARGET_BUILD_OS="windows";;
            *Msys)          TARGET_BUILD_OS="windows";;
            *)              TARGET_BUILD_OS="UNKNOWN:${uname_str}"
        esac

        if [[
            "$TARGET_BUILD_OS" != "windows" &&
            "$TARGET_BUILD_OS" != "macos" &&
            "$TARGET_BUILD_OS" != "linux"
        ]]; then
            echo "Unknown target build operating system: $TARGET_BUILD_OS"
            exit 1
        fi

        echo "Target build operating system: '$TARGET_BUILD_OS' (host)"
    else
        if [[
            "$TARGET_BUILD_OS" == "windows" ||
            "$TARGET_BUILD_OS" == "macos" ||
            "$TARGET_BUILD_OS" == "linux"
            ]]; then
            echo "Target build operating system: '$TARGET_BUILD_OS' (override)"
        else
            echo "Unknown '$TARGET_BUILD_OS' passed as first argument. Use 'host' to use the host build platform or use either: 'windows', 'macos', 'linux'."
            exit 1
        fi
    fi
}

function set_target_build_arch {
    if [[ -z "$TARGET_BUILD_ARCH" || $TARGET_BUILD_ARCH == "default" ]]; then
        if [[ "$TARGET_BUILD_OS" == "macos" ]]; then
            TARGET_BUILD_ARCH="x86_64;arm64"
        else
            TARGET_BUILD_ARCH="$(uname -m)"
        fi

        echo "Target build CPU architecture: '$TARGET_BUILD_ARCH' (default)"
    else
        if [[ "$TARGET_BUILD_ARCH" == "x86_64" || "$TARGET_BUILD_ARCH" == "arm64" ]]; then
            echo "Target build CPU architecture: '$TARGET_BUILD_ARCH' (override)"
        else
            echo "Unknown '$TARGET_BUILD_ARCH' passed as second argument. Use 'default' to use the host CPU architecture or use either: 'x86_64', 'arm64'."
            exit 1
        fi
    fi
    if [[ "$TARGET_BUILD_OS" == "macos" ]]; then
        CMAKE_ARCH_ARGS="-DCMAKE_OSX_ARCHITECTURES=$TARGET_BUILD_ARCH"
    fi
}

set_target_build_os
set_target_build_arch

function exit_if_last_command_failed() {
    error=$?
    if [ $error -ne 0 ]; then
        echo "Last command failed: $error"
        exit $error
    fi
}

function build_sokol() {
    echo "Building sokol..."
    SOKOL_BUILD_DIR="$DIR/cmake-build-release"
    cmake -S $DIR/src/c/sokol -B $SOKOL_BUILD_DIR $CMAKE_ARCH_ARGS \
        `# change output directories` \
        -DCMAKE_ARCHIVE_OUTPUT_DIRECTORY=$SOKOL_BUILD_DIR -DCMAKE_LIBRARY_OUTPUT_DIRECTORY=$SOKOL_BUILD_DIR -DCMAKE_RUNTIME_OUTPUT_DIRECTORY=$SOKOL_BUILD_DIR -DCMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE=$SOKOL_BUILD_DIR
    exit_if_last_command_failed
    cmake --build $SOKOL_BUILD_DIR --config Release
    exit_if_last_command_failed

    if [[ "$TARGET_BUILD_OS" == "linux" ]]; then
        SOKOL_LIBRARY_FILENAME="libsokol.so"
        SOKOL_LIBRARY_FILE_PATH_BUILD="$(readlink -f $SOKOL_BUILD_DIR/$SOKOL_LIBRARY_FILENAME)"
    elif [[ "$TARGET_BUILD_OS" == "macos" ]]; then
        SOKOL_LIBRARY_FILENAME="libsokol.dylib"
        SOKOL_LIBRARY_FILE_PATH_BUILD="$SOKOL_BUILD_DIR/$SOKOL_LIBRARY_FILENAME"
    elif [[ "$TARGET_BUILD_OS" == "windows" ]]; then
        SOKOL_LIBRARY_FILENAME="sokol.dll"
        SOKOL_LIBRARY_FILE_PATH_BUILD="$SOKOL_BUILD_DIR/$SOKOL_LIBRARY_FILENAME"
    fi
    SOKOL_LIBRARY_FILE_PATH="$LIB_DIR/$SOKOL_LIBRARY_FILENAME"

    if [[ ! -f "$SOKOL_LIBRARY_FILE_PATH_BUILD" ]]; then
        echo "The file '$SOKOL_LIBRARY_FILE_PATH_BUILD' does not exist!"
        exit 1
    fi

    mv "$SOKOL_LIBRARY_FILE_PATH_BUILD" "$SOKOL_LIBRARY_FILE_PATH"
    exit_if_last_command_failed
    echo "Copied '$SOKOL_LIBRARY_FILE_PATH_BUILD' to '$SOKOL_LIBRARY_FILE_PATH'"

    rm -r $SOKOL_BUILD_DIR
    exit_if_last_command_failed
    echo "Building sokol finished!"
}

build_sokol
ls -d "$LIB_DIR"/*

echo "Finished '$0'!"