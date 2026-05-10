#pragma once
#include "../Graph/GridGraph.h" //Dijkstra kördür.Haritayı ona veriyoruz.
#include "../Core/Metrics.h"    //C# arayüzü için verilecek bilgiler

class DijkstraSolver {
public:
    //En kısa yolu bulur ve rotadaki düğümlerin ID'lerini içeren dinamik bir dizi döndürür.
    static int* Solve(GridGraph* graph, int startId, int endId, int queueType, Metrics& outMetrics);
};
//queueType -> C#ta seçilen karşılaştırmalı veriyapılarından birini verir.