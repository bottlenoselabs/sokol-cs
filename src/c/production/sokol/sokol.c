#define SOKOL_IMPL
#define SOKOL_DLL
#define SOKOL_NO_ENTRY
#define SOKOL_NO_DEPRECATED
#define SOKOL_TRACE_HOOKS

#if _WIN32
    #pragma warning(disable : 5105)
    #ifndef _WIN64
        #error "Compiling for Windows 32-bit ARM or x86 is not supported."
    #endif
    #define WIN32_LEAN_AND_MEAN
    #define NOCOMM
    #ifndef WINAPI
        #define WINAPI __stdcall
    #endif
    #define APIENTRY WINAPI
    #define SOKOL_LOG(s) OutputDebugStringA(s)
    #include <windows.h>
#endif

#include "sokol.h"