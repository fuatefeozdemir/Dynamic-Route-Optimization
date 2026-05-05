cmake_minimum_required(VERSION 3.20)
project(RouteEngine)

set(CMAKE_CXX_STANDARD 17)

# ÖNEMLİ: Bu satır .exe yerine C# için gerekli olan .dll üretir.
add_library(RouteEngine SHARED
        API/EngineAPI.cpp
        Collections/Set.cpp
        Collections/Set.h
        Collections/Queue.cpp
        Collections/Queue.h
)

# DLL dosyasının nereye çıkacağını belirler
set_target_properties(RouteEngine PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build_dll"
)