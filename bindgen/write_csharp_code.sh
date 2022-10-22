#!/bin/bash
DIRECTORY="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

dotnet build "$DIRECTORY/../src/cs/production/Sokol.Bindgen/Sokol.Bindgen.csproj" -p:OutputPath="$DIRECTORY/plugins/Sokol.Bindgen"
c2cs cs