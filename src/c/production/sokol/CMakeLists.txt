cmake_minimum_required(VERSION 3.16)

project(sokol)

set(CMAKE_C_STANDARD 11)
set(CMAKE_CXX_STANDARD 11)

if (CMAKE_SYSTEM_NAME STREQUAL Linux)
    set(THREADS_PREFER_PTHREAD_FLAG ON)
    find_package(Threads REQUIRED)
endif()

get_filename_component(SOKOL_DIRECTORY_PATH "../../../../ext/sokol" REALPATH BASE_DIR "${CMAKE_CURRENT_SOURCE_DIR}")
get_filename_component(CIMGUI_DIRECTORY_PATH "../../../../ext/imgui-cs/cimgui" REALPATH BASE_DIR "${CMAKE_CURRENT_SOURCE_DIR}")
include_directories(${SOKOL_DIRECTORY_PATH} ${CIMGUI_DIRECTORY_PATH})

if (WIN32)
    add_definitions(-DSOKOL_D3D11)
    set(CMAKE_SHARED_LIBRARY_PREFIX "")
elseif (APPLE)
    add_definitions(-DSOKOL_METAL)
else()
    add_definitions(-DSOKOL_GLCORE33)
endif()

add_definitions(-DSOKOL_IMGUI_IMPL)
add_library(cimgui SHARED
    ${CIMGUI_DIRECTORY_PATH}/cimgui.h
    ${CIMGUI_DIRECTORY_PATH}/cimgui.cpp 
    ${CIMGUI_DIRECTORY_PATH}/imgui/imgui.h
    ${CIMGUI_DIRECTORY_PATH}/imgui/imgui.cpp
    ${CIMGUI_DIRECTORY_PATH}/imgui/imgui_widgets.cpp 
    ${CIMGUI_DIRECTORY_PATH}/imgui/imgui_draw.cpp 
    ${CIMGUI_DIRECTORY_PATH}/imgui/imgui_tables.cpp
    ${CIMGUI_DIRECTORY_PATH}/imgui/imgui_demo.cpp)
target_include_directories(cimgui INTERFACE cimgui)

add_library(sokol SHARED "sokol.c")
target_link_libraries(sokol cimgui)

if (WIN32)
    target_link_libraries(sokol -lkernel32)
    target_link_libraries(sokol -luser32)
    target_link_libraries(sokol -lole32)
    target_link_libraries(sokol -ld3d11)
elseif (APPLE)
    target_compile_options(sokol PRIVATE -x objective-c)
    target_link_libraries(sokol "-framework Cocoa")
    target_link_libraries(sokol "-framework QuartzCore")
    target_link_libraries(sokol "-framework Metal")
    target_link_libraries(sokol "-framework MetalKit")
    target_link_libraries(sokol "-framework AudioToolbox")
    set_target_properties(sokol PROPERTIES LINK_FLAGS "-Wl,-F/Library/Frameworks")
elseif (LINUX)
    target_link_libraries(sokol INTERFACE X11 Xi Xcursor GL dl m)
    target_link_libraries(sokol PUBLIC Threads::Threads)
endif()