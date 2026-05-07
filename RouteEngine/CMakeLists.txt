cmake_minimum_required(VERSION 3.20)
project(RouteEngine)

set(CMAKE_CXX_STANDARD 17)

# this file does not belong to any project target fix
# Klasördeki ve tüm alt klasörlerdeki bütün .cpp ve .h dosyalarını tarar ve SOURCE_FILES değişkenine atar.
file(GLOB_RECURSE SOURCE_FILES
        "*.cpp"
        "*.h"
)

# 2. ADIM: Manuel dosya listesi yerine bu otomatik toplanan listeyi kullanır.
# ÖNEMLİ: C# için gerekli olan .dll üretir.
add_library(RouteEngine SHARED ${SOURCE_FILES}
        Core/Metrics.h
        Core/Timer.cpp
        Core/Timer.h
        PriorityQueues/ArrayQueue.cpp
        PriorityQueues/ArrayQueue.h
        Collections/Stack.cpp
        Collections/Stack.h)

set_target_properties(RouteEngine PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build_dll"
        RUNTIME_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build_dll"
)