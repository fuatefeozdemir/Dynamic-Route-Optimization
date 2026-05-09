#include "EngineAPI.h"

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
}