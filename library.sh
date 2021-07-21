#!/bin/bash
#!/bin/bash
script_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
echo "Started building native library... Directory: $script_dir"

cmake -S $script_dir/src/c/sokol -B $script_dir/cmake-build-release
cmake --build $script_dir/cmake-build-release --config Release
mkdir -p "$script_dir/lib/"

uname_str="$(uname | tr '[:upper:]' '[:lower:]')"
if [[ "$uname_str" == "linux" ]]; then
    mv "$script_dir/cmake-build-release/libsokol.so" "$script_dir/lib/libsokol.so"
    echo "Moved $script_dir/cmake-build-release/libsokol.so to $script_dir/lib/libsokol.so"
elif [[ "$uname_str" == "darwin" ]]; then
    mv "$script_dir/cmake-build-release/libsokol.dylib" "$script_dir/lib/libsokol.dylib"
    echo "Moved $script_dir/cmake-build-release/libsokol.dylib to $script_dir/lib/libsokol.dylib"
fi

rm -r $script_dir/cmake-build-release
echo "Finished building native library..."