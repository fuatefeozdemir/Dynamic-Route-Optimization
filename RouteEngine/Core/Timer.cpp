#include "Timer.h"

// Constructor
Timer::Timer() {
    isRunning = false; // Başlangıçta çalışma durumu false
}

// Kronometreyi başlatır
void Timer::Start() {
    // İşlemcinin o anki vaktini al
    startTime = std::chrono::high_resolution_clock::now();
    isRunning = true;
}

// Eğer çalışıyorsa kronometreyi durdurur
void Timer::Stop() {
    if (isRunning) {
        endTime = std::chrono::high_resolution_clock::now();
        isRunning = false;
    }
}

// Aradaki farkı hesaplar ve mikrosaniye olarak verir
long long Timer::GetMicroseconds() {
    // 1. Durum: Kronometre hala çalışıyorsa
    if (isRunning) {
        // Şu anki canlı zamanı bitiş zamanı olarak al
        std::chrono::time_point<std::chrono::high_resolution_clock> end = std::chrono::high_resolution_clock::now();
        // Süreyi mikrosaniyeye çevir
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - startTime);
        return duration.count();
    }
    // 2. Durum: Kronometre durdurulmuşsa
    else {
        // Durdurulduğundaki kaydedilen bitiş zamanını kullan
        std::chrono::time_point<std::chrono::high_resolution_clock> end = endTime;
        // Süreyi mikrosaniyeye çevir
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - startTime);
        return duration.count();
    }
}