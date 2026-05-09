#pragma once
#include "../Graph/GridGraph.h"
#include "../Algorithms/DijkstraSolver.h"
#include "../Core/Metrics.h"

// C# (P/Invoke) ile uyumlu, isim karışıklığı (mangling) olmayan C tipi fonksiyonlar
extern "C" {
    // Haritayı dinamik bellek üzerinde (Heap) oluşturur ve adresini döndürür
    __declspec(dllexport) GridGraph* CreateGraph(int width, int height);

    // Haritayı bellekten silerek sızıntıyı önler
    __declspec(dllexport) void DeleteGraph(GridGraph* graph);

    // Bir hücrenin engel durumunu (duvar/yol) ID üzerinden değiştirir
    __declspec(dllexport) void ToggleObstacle(GridGraph* graph, int id);

    // Düğümler arasındaki komşuluk köprülerini (sağ, sol, üst, alt) inşa eder
    __declspec(dllexport) void BuildConnections(GridGraph* graph);

    // Dijkstra'yı tetikler. int* rotayı, outMetrics ise istatistikleri döndürür
    __declspec(dllexport) int* FindPath(GridGraph* graph, int startId, int endId, int queueType, Metrics* outMetrics);

    // C++'da 'new' ile ayrılan rota dizisini C# işi bitince temizlemek için kullanılır
    __declspec(dllexport) void DeletePath(int* path);
}