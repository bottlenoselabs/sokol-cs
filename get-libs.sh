#!/bin/bash

# Get the directory of this script
MY_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"
LIB_DIR=$MY_DIR/lib

 # Downloading
echo "Downloading latest sokol libraries ..."
curl -OL https://github.com/lithiumtoast/sokol-bin/releases/latest/download/win-x64.zip --compressed > "$MY_DIR/win-x64.zip"
if [ $? -ne 0 ]; then
    >&2 echo "ERROR: Unable to download successfully."
    exit 1
fi
curl -OL https://github.com/lithiumtoast/sokol-bin/releases/latest/download/osx-x64.zip --compressed > "$MY_DIR/osx-x64.zip"
if [ $? -ne 0 ]; then
    >&2 echo "ERROR: Unable to download successfully."
    exit 1
fi
curl -OL https://github.com/lithiumtoast/sokol-bin/releases/latest/download/linux-x64.zip --compressed > "$MY_DIR/linux-x64.zip"
if [ $? -ne 0 ]; then
    >&2 echo "ERROR: Unable to download successfully."
    exit 1
fi

echo "Finished downloading!"

# Decompressing
echo "Decompressing libraries ..."
rm -rf $LIB_DIR/libsokol
mkdir -p $LIB_DIR/libsokol
unzip $MY_DIR/win-x64.zip -d $LIB_DIR/libsokol 
unzip $MY_DIR/osx-x64.zip -d $LIB_DIR/libsokol 
unzip $MY_DIR/linux-x64.zip -d $LIB_DIR/libsokol 
if [ $? -ne 0 ]; then
    >&2 echo "ERROR: Unable to decompress successfully."
    exit 1
fi

echo "Finished decompressing libraries!"
rm $MY_DIR/win-x64.zip
rm $MY_DIR/osx-x64.zip
rm $MY_DIR/linux-x64.zip
