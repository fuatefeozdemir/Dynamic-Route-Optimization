#include "Node.h"

// Constructor:
// Bir Node oluşturulduğunda ilk değerleri burada atanır.
Node::Node(int _id, int _x, int _y) {
  //graftan geliyo
    id = _id;
    //Dışarıdan gelen X ve Y yi Point paketinin içine koyup coordinate degiskenine atıyoruz.
    coordinate = Point(_x, _y);
    isObstacle = false;         // Başlangıçta hiçbir kare engel değildir
    
    // Dijkstra algoritması için kural:
    // Başlangıçta tüm mesafeler sonsuz kabul edilir. 
    // Sonsuzluğu temsil etmesi için çok büyük bir sayı giriyoruz.
    //bunu max yapıyoruz kı daha kısa yolda guncelleyıp gecis yapsin
    distance = 999999.0f;       
    
    // Henüz hiçbir yerden gelinmediği için önceki düğüm -1 (Yok) kabul edilir.
    previousNodeId = -1;        
}

// OKUMA (GETTER) FONKSİYONLARI
int Node::GetId() { // int geri dönüş tipli
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

// DEĞİŞTİRME (SETTER) FONKSİYONLARI
// Dışarıdan gelen 'status' (true/false) değerini karenin içine işleriz
// Node sınıfındaki setIdObstacle fonksiyonu anlamına gelir.
void Node::SetIsObstacle(bool status) {
    isObstacle = status;
}

// Dijkstra algoritması daha kısa bir yol bulduğunda bu fonksiyonu çağırıp mesafeyi günceller.
void Node::SetDistance(float _distance) {
    distance = _distance;
}

// Rotayı oluştururken hangi kareden geldiği bilgisini yazar.
void Node::SetPreviousNodeId(int _prevId) {
    previousNodeId = _prevId;
}