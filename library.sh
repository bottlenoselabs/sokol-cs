#!/bin/bash
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

$DIRECTORY/ext/scripts/c/library/main.sh \
    $DIRECTORY/src/c/production/sokol \
    $DIRECTORY/build \
    $DIRECTORY/lib \
    "sokol" \
    "sokol" \
    "" \
    ""

if [[ -z "$1" ]]; then
    read
fi