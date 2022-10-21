#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

$DIR/ext/scripts/c/library/main.sh \
    $DIR/src/c/sokol \
    $DIRECTORY/build \
    $DIR/lib \
    "sokol" \
    "sokol" \
    "" \
    ""

if [[ -z "$1" ]]; then
    read
fi