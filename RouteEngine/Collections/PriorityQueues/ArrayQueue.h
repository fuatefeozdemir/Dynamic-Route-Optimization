#pragma once

// Kuyrukta bekleyen her bir karenin ID'sini ve o anki bilinen en kısa mesafesini tutar
struct ArrayItem {
    int id;
    int distance;
};

class ArrayQueue {
private:
    ArrayItem* items; // Dinamik dizi
    int capacity;     // Dizinin maksimum boyutu
    int currentSize;  // O an kuyrukta kaç eleman var

public:
    ArrayQueue(int maxCapacity);
    ~ArrayQueue();

    void Insert(int id, int distance);            // Kuyruğa yeni keşfedilen bir yol ekle
    int ExtractMin();                             // Mesafesi EN KISA olanı bul, sil ve ver
    void UpdateDistance(int id, int newDistance); // Daha kısa bir alternatif yol bulunursa güncelle
    bool IsEmpty() const;
    void Clear();
};