#pragma once
#include "../Graph/GridGraph.h"
#include "../Core/Metrics.h"

class DijkstraSolver {
public:
    //En kısa yolu bulur ve rotadaki düğümlerin ID'lerini içeren dinamik bir dizi döndürür.
    static int* Solve(GridGraph* graph, int startId, int endId, int queueType, Metrics& outMetrics);
};