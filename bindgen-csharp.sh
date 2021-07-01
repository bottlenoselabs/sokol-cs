#!/bin/bash

unamestr="$(uname | tr '[:upper:]' '[:lower:]')"
if [ "$unamestr" == "darwin" ] || [[ "$unamestr" == "linux" ]]; then
    # sokol_app
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_app.h -o ./sokol_app.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_app.json -o ./src/cs/production/sokol-cs/sokol_app.cs -l "sokol"
    rm ./sokol_app.json
    # sokol_gfx
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_gfx.h -o ./sokol_gfx.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_gfx.json -o ./src/cs/production/sokol-cs/sokol_gfx.cs -a "sg_color->Rgba32F" -g Rgba32F -l "sokol"
    rm ./sokol_gfx.json
    # sokol_glue
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_glue.h -o ./sokol_glue.json -s ./ext/sokol -a "--include=sokol_gfx.h" "--include=sokol_app.h" -g sokol_gfx.h sokol_app.h
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_glue.json -o ./src/cs/production/sokol-cs/sokol_glue.cs -l "sokol"
    rm ./sokol_glue.json
    ## Dirty trick :)
    sed -i .bak -e "s/^using C2CS;/using C2CS;\nusing static sokol_gfx;/" ./src/cs/production/sokol-cs/sokol_glue.cs
    rm ./src/cs/production/sokol-cs/sokol_glue.cs.bak
    # sokol_audio
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_audio.h -o ./sokol_audio.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_audio.json -o ./src/cs/production/sokol-cs/sokol_audio.cs -l "sokol"
    rm ./sokol_audio.json
    # sokol_fetch
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_fetch.h -o ./sokol_fetch.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_fetch.json -o ./src/cs/production/sokol-cs/sokol_fetch.cs -l "sokol"
    rm ./sokol_fetch.json
    # sokol_time
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_time.h -o ./sokol_time.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_time.json -o ./src/cs/production/sokol-cs/sokol_time.cs -l "sokol"
    rm ./sokol_time.json
    # sokol_args
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj ast -i ./ext/sokol/sokol_args.h -o ./sokol_args.json -s ./ext/sokol
    dotnet run --project ./ext/C2CS/src/cs/production/C2CS/C2CS.csproj cs -i ./sokol_args.json -o ./src/cs/production/sokol-cs/sokol_args.cs -l "sokol"
    rm ./sokol_args.json
else
    echo "Unknown platform: '$unamestr'."
fi

