call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars64.bat"
cmake -S .\src\c\sokol -B .\cmake-build-release -G "Visual Studio 16 2019" -DCMAKE_CONFIGURATION_TYPES=Release
devenv .\cmake-build-release\sokol.sln /build