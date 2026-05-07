#pragma once
#include "Point.h"

class Node {
private:
    int id;                 // Karenin no'su (Bu id, LinkedList'teki TargetNodeId'dir)
    Point coordinate;       // Karenin X ve Y koordinatı
    bool isObstacle;        // Bu kare bir engel mi?
    int distance;           // Dijkstra'nın başlangıçtan buraya gelme maliyeti (distance, o yoldaki weight'lerin toplamıdır)
    int previousNodeId;     // Rotayı bulduktan sonra geriye doğru çizmek için bir önceki adımın ID'si

public:
    // Constructor
    Node(int _id, int _x, int _y);

    // Getter Metotlar
    int GetId();
    Point GetCoordinate();
    bool GetIsObstacle();
    int GetDistance();
    int GetPreviousNodeId();

    // Setter Metotlar
    void SetIsObstacle(bool status);
    void SetDistance(int _distance);
    void SetPreviousNodeId(int _prevId);
};