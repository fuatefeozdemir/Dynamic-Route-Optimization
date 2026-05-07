#include "Node.h"

// Constructor
Node::Node(int _id, int _x, int _y) {
    // Graftan geliyo
    id = _id;
    // Dışarıdan gelen X ve Y yi Point paketinin içine koyup coordinate degiskenine atıyoruz.
    coordinate = Point(_x, _y);
    isObstacle = false;         // Başlangıçta hiçbir kare engel değildir
    
    // Dijkstra algoritmasında başlangıçta tüm mesafeler sonsuz kabul edilir
    // Sonsuzluğu temsil etmesi için çok büyük bir sayı giriyoruz.
    // Bunu max yapıyoruz kı daha kısa yolda guncelleyıp gecis yapsin
    distance = 999999.0f;       
    
    // Henüz hiçbir yerden gelinmediği için önceki düğüm -1 (Yok) kabul edilir.
    previousNodeId = -1;        
}

// Getter Metotlar
int Node::GetId() {
    return id;
}

Point Node::GetCoordinate() {
    return coordinate;
}

bool Node::GetIsObstacle() {
    return isObstacle;
}

int Node::GetDistance() {
    return distance;
}

int Node::GetPreviousNodeId() {
    return previousNodeId;
}

// Setter Metotlar
void Node::SetIsObstacle(bool status) {
    isObstacle = status;
}

// Dijkstra algoritması daha kısa bir yol bulduğunda bu fonksiyonu çağırıp mesafeyi günceller.
void Node::SetDistance(int _distance) {
    distance = _distance;
}

// Rotayı oluştururken hangi kareden geldiği bilgisini yazar.
void Node::SetPreviousNodeId(int _prevId) {
    previousNodeId = _prevId;
}