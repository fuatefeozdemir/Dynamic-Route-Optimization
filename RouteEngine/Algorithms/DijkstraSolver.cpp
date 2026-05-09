#include "DijkstraSolver.h"
#include "../Core/Timer.h"
#include "../Collections/PriorityQueues/ArrayQueue.h"
#include "../Collections/PriorityQueues/MinHeap.h"
#include "../Collections/PriorityQueues/BSTQueue.h"
#include "../Collections/Stack.h"
#include "../Collections/Queue.h"

const int INFINITY_DIST = 999999;

int* DijkstraSolver::Solve(GridGraph* graph, int startId, int endId, int queueType, Metrics& outMetrics) {
    int totalNodes = graph->GetTotalNodes();

    // 1. ADIM: HARİTAYI TEMİZLE
    for (int i = 0; i < totalNodes; i++) {
        Node* node = graph->GetNode(i);
        if (node != nullptr) {
            node->SetDistance(INFINITY_DIST);
            node->SetPreviousNodeId(-1);
        }
    }

    // Başlangıç noktasını hazırla
    Node* startNode = graph->GetNode(startId);
    if (startNode) startNode->SetDistance(0);

    // Metrikleri başlat
    Timer timer;
    timer.Start();
    int nodesExamined = 0;
    outMetrics.routeFound = false;
    outMetrics.visitedNodes = nullptr; // YENİ: Başlangıçta boş
    outMetrics.visitedCount = 0;       // YENİ: Başlangıçta 0

    Queue animationQueue;

    // 2. ADIM: KUYRUK SEÇİMİ
    ArrayQueue* arrayQueue = nullptr;
    MinHeap* minHeap = nullptr;
    BSTQueue* bstQueue = nullptr;

    if (queueType == 1) {
        arrayQueue = new ArrayQueue(totalNodes);
        arrayQueue->Insert(startId, 0);
    } else if (queueType == 2) {
        minHeap = new MinHeap(totalNodes);
        minHeap->Push(startId, 0.0f);
    } else if (queueType == 3) {
        bstQueue = new BSTQueue();
        bstQueue->Insert(startId, 0.0);
    }

    // 3. ADIM: ANA ARAMA DÖNGÜSÜ
    while (true) {
        int currentId = -1;

        // Seçilen yapıdan en yakın düğümü çek
        if (queueType == 1) {
            if (arrayQueue->IsEmpty()) break;
            currentId = arrayQueue->ExtractMin();
        } else if (queueType == 2) {
            if (minHeap->IsEmpty()) break;
            currentId = minHeap->ExtractMin();
        } else if (queueType == 3) {
            if (bstQueue->IsEmpty()) break;
            currentId = bstQueue->ExtractMin();
        }

        if (currentId == -1) break;

        animationQueue.Enqueue(currentId);

        // Hedefe ulaştık mı?
        if (currentId == endId) {
            outMetrics.routeFound = true;
            break;
        }

        nodesExamined++;
        Node* currentNode = graph->GetNode(currentId);
        int currentDist = currentNode->GetDistance();

        // 4. ADIM: KOMŞULARI GEZ
        LinkedList* neighborsList = graph->GetNeighbors(currentId);
        ListNode* neighborNode = neighborsList->GetHead();

        while (neighborNode != nullptr) {
            int neighborId = neighborNode->TargetNodeId;
            float weight = neighborNode->Weight;

            Node* neighbor = graph->GetNode(neighborId);

            // Engel kontrolü
            if (neighbor->GetIsObstacle()) {
                neighborNode = neighborNode->Next;
                continue;
            }

            int newDist = currentDist + (int)weight;

            // Gevşetme (Relaxation)
            if (newDist < neighbor->GetDistance()) {
                neighbor->SetDistance(newDist);
                neighbor->SetPreviousNodeId(currentId);

                if (queueType == 1) {
                    arrayQueue->Insert(neighborId, newDist);
                } else if (queueType == 2) {
                    minHeap->Push(neighborId, (float)newDist);
                } else if (queueType == 3) {
                    bstQueue->Insert(neighborId, (double)newDist);
                }
            }
            neighborNode = neighborNode->Next;
        }
    }

    timer.Stop();
    outMetrics.timeMicroseconds = timer.GetMicroseconds();
    outMetrics.nodesExamined = nodesExamined;

    // Animasyon verisini C#'a göndermek için Metrics dizisine aktarıyoruz
    outMetrics.visitedCount = nodesExamined + (outMetrics.routeFound ? 1 : 0);
    if (outMetrics.visitedCount > 0) {
        outMetrics.visitedNodes = new int[outMetrics.visitedCount];
        for (int i = 0; i < outMetrics.visitedCount; i++) {
            outMetrics.visitedNodes[i] = animationQueue.Dequeue();
        }
    }

    // 5. ADIM: BELLEK TEMİZLİĞİ VE ROTA OLUŞTURMA
    if (!outMetrics.routeFound) {
        outMetrics.pathLength = 0;
        if (arrayQueue) delete arrayQueue;
        if (minHeap) delete minHeap;
        if (bstQueue) delete bstQueue;
        return nullptr;
    }

    // Stack ile yolu tersine çevir
    Stack pathStack;
    int stepId = endId;
    while (stepId != -1) {
        pathStack.Push(stepId);
        stepId = graph->GetNode(stepId)->GetPreviousNodeId();
    }

    int count = 0;
    Stack tempStack;
    while (!pathStack.IsEmpty()) {
        tempStack.Push(pathStack.Pop());
        count++;
    }
    outMetrics.pathLength = count;

    int* finalPath = new int[count];
    for (int i = 0; i < count; i++) {
        finalPath[i] = tempStack.Pop();
    }

    if (arrayQueue) delete arrayQueue;
    if (minHeap) delete minHeap;
    if (bstQueue) delete bstQueue;

    return finalPath;
}