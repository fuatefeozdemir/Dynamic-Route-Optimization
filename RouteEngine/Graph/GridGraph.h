#pragma once
#include "../Core/Node.h"               // Senin yazdığın Node sınıfını çağırıyoruz
#include "../Collections/LinkedList.h"  // Dev 2'nin yazdığı LinkedList sınıfını çağırıyoruz

class GridGraph {
private:
    // 1. HARİTANIN FİZİKSEL ÖZELLİKLERİ
    int width;   // Haritanın genişliği (Örn: 50)
    int height;  // Haritanın yüksekliği (Örn: 50)

    // HAFIZA (RAM) YÖNETİMİ
    // Node ve LinkedList nesnelerini tutacak dinamik diziler (Pointer Array)
    Node** nodes;                 // Tüm hücreleri tutacağımız depo
    LinkedList* adjacencyList;    // Her hücrenin komşularını tutacağımız depo

public:
    // ==========================================
    // ZEYNEP
    // ==========================================
    
    // Kurucu Metot: Haritayı genişlik ve yüksekliğe göre inşa eder
    GridGraph(int _width, int _height);

    // ID vererek o hücreyi (Node) getiren fonksiyon
    Node* GetNode(int id);

    // X ve Y koordinatı vererek o hücreyi (Node) getiren fonksiyon
    Node* GetNode(int x, int y);

    // Arayüzden tıklanan hücreyi Duvar/Yol yapan fonksiyon
    void ToggleObstacle(int id);


    // ==========================================
    // SUDE
    // ==========================================
    
    // Yıkıcı Metot: Program kapanırken RAM'i temizler
    ~GridGraph();

    // Düğümler arasındaki komşuluk (sağ/sol/üst/alt) köprülerini kurar
    void BuildConnections();

    // Algoritma sorduğunda o hücrenin gidebileceği komşu yolları verir
    LinkedList* GetNeighbors(int id);
};