#pragma once
#include "../Graph/GridGraph.h"
#include "../Core/Metrics.h"

extern "C" {
    // Harita nesnesini belleğe yerleştirir
    __declspec(dllexport) GridGraph* CreateGraph(int width, int height);

    // Bellek temizliği
    __declspec(dllexport) void DeleteGraph(GridGraph* graph);

    // Engel geçişi
    __declspec(dllexport) void ToggleObstacle(GridGraph* graph, int id);

    // Komşulukları hazırla
    __declspec(dllexport) void BuildConnections(GridGraph* graph);

    // Ana algoritma çağrısı
    __declspec(dllexport) int* FindPath(GridGraph* graph, int startId, int endId, int queueType, Metrics* outMetrics);

    // C#'ın silemediği Rota dizisini temizler
    __declspec(dllexport) void DeletePath(int* path);

    // C#'ın silemediği Animasyon (Visited) dizisini temizler
    __declspec(dllexport) void DeleteVisitedNodes(int* visitedNodes);

    // C# döngüsünden kurtulmak için, verilen yüzde ihtimalle C++ içinde rastgele engel üretir.
    __declspec(dllexport) int* GenerateRandomObstacles(GridGraph* graph, int probabilityPercent, int* outCount);

    // Geri al tuşu için tüm engelleri tek seferde temizler.
    __declspec(dllexport) void ClearAllObstacles(GridGraph* graph);

    // Yeni harita boyutları girildiğinde objeyi silmek yerine içini temizleyip yeniden boyutlandırır.
    __declspec(dllexport) void ResetGraph(GridGraph* graph, int newWidth, int newHeight);

    // Verilen ID listesindeki engelleri topluca kaldırır ve haritayı günceller
    __declspec(dllexport) void RemoveObstaclesBatch(GridGraph* graph, int* ids, int count);

    // C++'ın oluşturup C#'a gönderdiği int dizilerini (engeller listesi vb.) iş bitince RAM'den silmek için
    __declspec(dllexport) void FreeIntArray(int* arrayPtr);
}