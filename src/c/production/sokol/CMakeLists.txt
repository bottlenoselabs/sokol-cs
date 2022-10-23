cmake_minimum_required(VERSION 3.16)

project(sokol)

set(CMAKE_C_STANDARD 99)

get_filename_component(SOKOL_DIRECTORY_PATH "../../../../ext/sokol" REALPATH BASE_DIR "${CMAKE_CURRENT_SOURCE_DIR}")
get_filename_component(CIMGUI_DIRECTORY_PATH "../../../../ext/cimgui" REALPATH BASE_DIR "${CMAKE_CURRENT_SOURCE_DIR}")
include_directories(${SOKOL_DIRECTORY_PATH} ${CIMGUI_DIRECTORY_PATH})

set (CMAKE_SYSTEM_VERSION 10.0 (latest installed version))

if (WIN32)
    add_definitions(-DSOKOL_D3D11)
    set(CMAKE_SHARED_LIBRARY_PREFIX "")
elseif (APPLE)
    add_definitions(-DSOKOL_METAL)
else()
    add_definitions(-DSOKOL_GLCORE33)
endif()

if (APPLE)
    set(SOURCES "sokol.m")
else()
    set(SOURCES "sokol.c")
endif()

add_library(sokol SHARED ${SOURCES})

if (WIN32)
    target_link_libraries(sokol -lkernel32)
    target_link_libraries(sokol -luser32)
    target_link_libraries(sokol -lole32)
    target_link_libraries(sokol -ld3d11)
elseif (APPLE)
    target_link_libraries(sokol "-framework Cocoa")
    target_link_libraries(sokol "-framework QuartzCore")
    target_link_libraries(sokol "-framework Metal")
    target_link_libraries(sokol "-framework MetalKit")
    target_link_libraries(sokol "-framework AudioToolbox")
    set_target_properties(sokol PROPERTIES LINK_FLAGS "-Wl,-F/Library/Frameworks")
endif()