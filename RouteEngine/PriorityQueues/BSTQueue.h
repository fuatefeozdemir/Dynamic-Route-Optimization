#pragma once
#include <iostream>

// Ağacın her bir halkası (Düğümü)
struct BSTNode {
    int id;             //Her zamanki id
    double distance;    // Başlangıca(roota) olan mesafe (Ağaç buna göre sıralanacak)
    BSTNode* left;      // Daha küçük mesafeler sola
    BSTNode* right;     // Daha büyük mesafeler sağa

    BSTNode(int _id, double _dist)
        : id(_id), distance(_dist), left(nullptr), right(nullptr) {}
    //disardan gelen atama ksımı
};

class BSTQueue {
private:
    BSTNode* root;
//agaca ulasmak ıcın kapı

    //agacın kendı ısleyıs yapısı kendı ıcınde ugrasıyo
    // Yardımcı fonksiyonlar (Sadece sınıf içinden erişilir)
    BSTNode* insertRecursive(BSTNode* node, int id, double distance);
    //djikstra ekle dedikten sonra burası kendı mat duzenler
    void destroyTree(BSTNode* node);//temizlık

public:
    //dış dunyaya
    BSTQueue();
    ~BSTQueue();

    void Insert(int id, double distance); // Yeni bir hücreyi mesafesiyle ekle
    int ExtractMin();                     // En küçük mesafeyi bul, sil ve ID'sini döndür
    bool IsEmpty();                       // Ağaç boş mu?
};