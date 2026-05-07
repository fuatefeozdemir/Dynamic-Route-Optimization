#pragma once
#include "../Core/Node.h"               // Aynı şekilde node sınıfı
#include "../Collections/LinkedList.h"  //Aynı dosya icinde olmayinca direkt include diyemiyoruz collection dosyasina git dedik

class GridGraph {
private:
    int width;   //Kaç satır
    int height;  //kaç sütun

    //HAFIZA (RAM) YÖNETİMİ
    //Node ve LinkedList nesnelerini tutacak dinamik diziler (Pointer Array)
    Node** nodes;                 //Tüm hücreleri tutacağımız depo //X, Y koordinatları ve engel olup olmadığını saklar
    LinkedList* adjacencyList;    //Her hücrenin komşularını tutacağımız depo
    //Hücrelerin birbirleriyle olan geçerli hareket yolları komşular.
public:

    // Constructor
    GridGraph(int _width, int _height);

//LinkedList teki idlere gore kutunun ozelliklerini(dolu,boş..)kontrol eder
    // ID vererek o hücreyi (Node) getiren fonksiyon
    Node* GetNode(int id);
//bu da ıd ile degıl konumla buluyo
    // X ve Y koordinatı vererek o hücreyi (Node) getiren fonksiyon
    Node* GetNode(int x, int y);

    //Arayüzden tıklanan hücreyi Duvar/Yol yapan fonksiyon
    void ToggleObstacle(int id);

    // Yıkıcı Metot: Program kapanırken RAM'i temizler
    ~GridGraph();//memory leak onler

    // Düğümler arasındaki komşuluk (sağ/sol/üst/alt) köprülerini kurar
    void BuildConnections();//duvar var mı bakıp yoksa bir yol ekleyıp lınked lıste eklıyoruz varsa oyle kopuk kalıyo

    // Algoritma sorduğunda o hücrenin gidebileceği komşu yolları verir
    LinkedList* GetNeighbors(int id);
    //linkedlist başta bomboş graph eklıyo ıd numarası ıle
    //bu komsuları o buyuk harıtada sag sol ust alt bakarak arıyo
    /*djıkstra da o harıtadakı karoları goremıyo dıye adım atabılecegını lınked lıste yazıyo
     *ordan djısktra anlıyo yanı tamam bu ekledıgı duvar degıl sen bu ıd lı karoya adım atabılırsın yol cızdım
   */
};