#pragma once

struct Metrics {
    long long timeMicroseconds; // Algoritmanın çalışma süresi (Mikrosaniye)
    int nodesExamined;          // Hedefi bulana kadar kaç kareye baktı (Set sınıfına kaç eleman eklendi)
    int pathLength;             // Bulunan en kısa yolun kaç kare/adım olduğu
    bool routeFound;            // Yol bulunabildi mi? (Hedef duvarların içindeyse false döner)

    int* visitedNodes;          // İncelenen düğümlerin sırasıyla ID'leri
    int visitedCount;           // Dizi boyutu
};