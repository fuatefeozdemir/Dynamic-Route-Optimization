#include "Timer.h"

Timer::Timer() {
    isRunning = false;
}

// Kronometreyi başlat
void Timer::Start() {
    // İşlemcinin o anki tam vaktini al
    startTime = std::chrono::high_resolution_clock::now();
    isRunning = true;
}

// Kronometreyi durdur
void Timer::Stop() {
    if (isRunning) {
        endTime = std::chrono::high_resolution_clock::now();
        isRunning = false;
    }
}

// Aradaki farkı hesapla ve mikrosaniye olarak ver
long long Timer::GetMicroseconds() const {
    // Eğer kronometre durmamışsa hata verme, şu anki zamana göre farkı al
    auto end = isRunning ? std::chrono::high_resolution_clock::now() : endTime;
    
    // Süreyi mikrosaniyeye çevir (1 saniye = 1.000.000 mikrosaniye)
    auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - startTime);
    return duration.count();
}