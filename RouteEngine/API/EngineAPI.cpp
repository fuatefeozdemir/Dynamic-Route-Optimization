#include "EngineAPI.h"
#include "../Algorithms/DijkstraSolver.h"

// IDE ve derleyici uyumluluğu için
#ifdef _WIN32
    #define EXPORT_API __declspec(dllexport)
#else
    #define EXPORT_API __attribute__((visibility("default")))
#endif

extern "C" {
    // Harita nesnesini belleğe yerleştirir
    EXPORT_API GridGraph* CreateGraph(int width, int height) {
        return new GridGraph(width, height);
    }

    // Bellek temizliği
    EXPORT_API void DeleteGraph(GridGraph* graph) {
        if (graph != nullptr) {
            delete graph;
        }
    }

    // Engel geçişi
    EXPORT_API void ToggleObstacle(GridGraph* graph, int id) {
        if (graph != nullptr) {
            graph->ToggleObstacle(id);
        }
    }

    // Komşulukları hazırla
    EXPORT_API void BuildConnections(GridGraph* graph) {
        if (graph != nullptr) {
            graph->BuildConnections();
        }
    }

    // Ana algoritma çağrısı
    EXPORT_API int* FindPath(GridGraph* graph, int startId, int endId, int queueType, Metrics* outMetrics) {
        if (graph == nullptr || outMetrics == nullptr) {
            return nullptr;
        }
        return DijkstraSolver::Solve(graph, startId, endId, queueType, *outMetrics);
    }

    // C#'ın silemediği Rota dizisini temizler
    EXPORT_API void DeletePath(int* path) {
        if (path != nullptr) {
            delete[] path;
        }
    }

    // C#'ın silemediği Animasyon (Visited) dizisini temizler
    EXPORT_API void DeleteVisitedNodes(int* visitedNodes) {
        if (visitedNodes != nullptr) {
            delete[] visitedNodes;
        }
    }

    // C# döngüsünden kurtulmak için, verilen yüzde ihtimalle C++ içinde rastgele engel üretir.
    EXPORT_API int* GenerateRandomObstacles(GridGraph* graph, int probabilityPercent, int* outCount) {
        if (graph != nullptr && outCount != nullptr) {
            return graph->GenerateRandomObstacles(probabilityPercent, *outCount);
        }
        if (outCount != nullptr) *outCount = 0;
            return nullptr;
    }

    // Geri al tuşu için tüm engelleri tek seferde temizler.
    EXPORT_API void ClearAllObstacles(GridGraph* graph) {
        if (graph != nullptr) {
            graph->ClearAllObstacles();
        }
    }

    // Yeni harita boyutları girildiğinde objeyi silmek yerine içini temizleyip yeniden boyutlandırır.
    EXPORT_API void ResetGraph(GridGraph* graph, int newWidth, int newHeight) {
        if (graph != nullptr) {
            graph->ResetGraph(newWidth, newHeight);
        }
    }

    // Verilen ID listesindeki engelleri topluca kaldırır ve haritayı günceller
    EXPORT_API void RemoveObstaclesBatch(GridGraph* graph, int* ids, int count) {
        if (graph != nullptr && ids != nullptr && count > 0) {
            graph->RemoveObstaclesBatch(ids, count);
        }
    }

    // C++'ın oluşturup C#'a gönderdiği int dizilerini (engeller listesi vb.) iş bitince RAM'den silmek için
    EXPORT_API void FreeIntArray(int* arrayPtr) {
        if (arrayPtr != nullptr) {
            delete[] arrayPtr;
        }
    }
}