#!/bin/bash

unamestr="$(uname | tr '[:upper:]' '[:lower:]')"
if [ "$unamestr" == "darwin" ] || [[ "$unamestr" == "linux" ]]; then
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./src/c/sokol/sokol.h -o ./ast.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./ast.json -o ./src/cs/production/sokol-cs/sokol.cs -a "sg_color->Rgba32F" -g Rgba32F
    rm ./ast.json
else
    echo "Unknown platform: '$unamestr'."
fi

