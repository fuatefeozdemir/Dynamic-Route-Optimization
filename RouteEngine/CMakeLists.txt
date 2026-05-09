cmake_minimum_required(VERSION 3.20)
project(RouteEngine)

set(CMAKE_CXX_STANDARD 17)

# Klasördeki ve tüm alt klasörlerdeki bütün .cpp ve .h dosyalarını otomatik bulur
file(GLOB_RECURSE SOURCE_FILES
        "*.cpp"
        "*.h"
)

# Sadece otomatik toplanan SOURCE_FILES değişkenini kullanarak DLL oluşturur
add_library(RouteEngine SHARED ${SOURCE_FILES}
        Algorithms/DijkstraSolver.cpp
        Algorithms/DijkstraSolver.h)

# Üretilen DLL dosyasını karmaşa yaratmaması için build_dll klasörüne atar
set_target_properties(RouteEngine PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build_dll"
        RUNTIME_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build_dll"
)

# DLL'in ihtiyaç duyduğu MinGW kütüphanelerini içine gömer
target_link_options(RouteEngine PRIVATE -static -static-libgcc -static-libstdc++)