#include "EngineAPI.h"

extern "C" {
    // Harita nesnesini belleğe yerleştirir
    __declspec(dllexport) GridGraph* CreateGraph(int width, int height) {
        return new GridGraph(width, height);
    }

    // Bellek temizliği: C++ tarafında 'new' ile oluşturulan nesne burada silinir
    __declspec(dllexport) void DeleteGraph(GridGraph* graph) {
        if (graph != nullptr) {
            delete graph;
        }
    }

    // GridGraph üzerindeki ToggleObstacle metoduna yönlendirme yapar
    __declspec(dllexport) void ToggleObstacle(GridGraph* graph, int id) {
        if (graph != nullptr) {
            graph->ToggleObstacle(id);
        }
    }

    // Dijkstra çalışmadan önce komşuluk listesini hazırlar
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

        // Algoritma çalışır, içindeki metrics struct'ını doldurur
        // ve rota ID dizisinin pointer'ını döndürür.
        return DijkstraSolver::Solve(graph, startId, endId, queueType, *outMetrics);
    }

    // C#'ın silemediği C++ dizisi burada silinir
    __declspec(dllexport) void DeletePath(int* path) {
        if (path != nullptr) {
            delete[] path;
        }
    }
}