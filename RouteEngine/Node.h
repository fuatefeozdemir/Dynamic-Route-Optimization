#pragma once
#include "Point.h" // Point struct'ımızı bu dosyaya dahil ediyoruz

class Node {
private:
    //Sadece bu sınıfın içinden erişilebilir
    int id;                 // Karenin no'su
    Point coordinate;       // Karenin X ve Y koordinatı
    bool isObstacle;        // Bu kare bir duvar/engel mi?
    float distance;         // Dijkstra'nın buraya gelme maliyeti
    int previousNodeId;     // Rotayı bulduktan sonra geriye doğru çizmek için bir önceki adımın ID'si

public:
    // public fonksiyonlar (API)

    // Constructor
    Node(int _id, int _x, int _y);

    // Okuma (Getter) Fonksiyonları
    int GetId();
    Point GetCoordinate();
    bool GetIsObstacle();
    float GetDistance();
    int GetPreviousNodeId();

    // Değiştirme (Setter) Fonksiyonları
    void SetIsObstacle(bool status);
    void SetDistance(float _distance);
    void SetPreviousNodeId(int _prevId);
    //setId ve setCoordinate fonskiyonu yazmadık çünkü o karenin id si ve koordinatı akış boyunca değiştirilmemesi gerek.
};