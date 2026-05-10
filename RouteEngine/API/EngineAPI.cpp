#include "EngineAPI.h"
#include "../Algorithms/DijkstraSolver.h" // Sende include eksik olabilir diye ekledim

extern "C" {
    // Harita nesnesini belleğe yerleştirir
    __declspec(dllexport) GridGraph* CreateGraph(int width, int height) {
        return new GridGraph(width, height);
    }

    // Bellek temizliği
    __declspec(dllexport) void DeleteGraph(GridGraph* graph) {
        if (graph != nullptr) {
            delete graph;
        }
    }

    // Engel geçişi
    __declspec(dllexport) void ToggleObstacle(GridGraph* graph, int id) {
        if (graph != nullptr) {
            graph->ToggleObstacle(id);
        }
    }

    // Komşulukları hazırla
    __declspec(dllexport) void BuildConnections(GridGraph* graph) {
        if (graph != nullptr) {
            graph->BuildConnections();
        }
    }

    // Ana algoritma çağrısı
    __declspec(dllexport) int* FindPath(GridGraph* graph, int startId, int endId, int queueType, Metrics* outMetrics) {
        if (graph == nullptr || outMetrics == nullptr) {
            return nullptr;
        }
        return DijkstraSolver::Solve(graph, startId, endId, queueType, *outMetrics);
    }

    // C#'ın silemediği Rota dizisini temizler
    __declspec(dllexport) void DeletePath(int* path) {
        if (path != nullptr) {
            delete[] path;
        }
    }

    // C#'ın silemediği Animasyon (Visited) dizisini temizler
    __declspec(dllexport) void DeleteVisitedNodes(int* visitedNodes) {
        if (visitedNodes != nullptr) {
            delete[] visitedNodes;
        }
    }

    // C# döngüsünden kurtulmak için, verilen yüzde ihtimalle C++ içinde rastgele engel üretir.
    __declspec(dllexport) int* GenerateRandomObstacles(GridGraph* graph, int probabilityPercent, int* outCount) {
        if (graph != nullptr && outCount != nullptr) {
            return graph->GenerateRandomObstacles(probabilityPercent, *outCount);
        }
        if (outCount != nullptr) *outCount = 0;
        return nullptr;
    }

    // Geri al tuşu için tüm engelleri tek seferde temizler.
    __declspec(dllexport) void ClearAllObstacles(GridGraph* graph) {
        if (graph != nullptr) {
            graph->ClearAllObstacles();
        }
    }

    // Yeni harita boyutları girildiğinde objeyi silmek yerine içini temizleyip yeniden boyutlandırır.
    __declspec(dllexport) void ResetGraph(GridGraph* graph, int newWidth, int newHeight) {
        if (graph != nullptr) {
            graph->ResetGraph(newWidth, newHeight);
        }
    }

    // Verilen ID listesindeki engelleri topluca kaldırır ve haritayı günceller
    __declspec(dllexport) void RemoveObstaclesBatch(GridGraph* graph, int* ids, int count) {
        if (graph != nullptr && ids != nullptr && count > 0) {
            graph->RemoveObstaclesBatch(ids, count);
        }
    }

    // C++'ın oluşturup C#'a gönderdiği int dizilerini (engeller listesi vb.) iş bitince RAM'den silmek için
    __declspec(dllexport) void FreeIntArray(int* arrayPtr) {
        if (arrayPtr != nullptr) {
            delete[] arrayPtr;
        }
    }
}