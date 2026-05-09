#pragma once

// Kuyrukta bekleyen her bir karenin ID'sini ve o anki bilinen en kısa mesafesini tutar
struct ArrayItem {
    int id; //haritadaki karenin numarası
    int distance;   //Başlangıç noktasından bu kareye olan total uzaklık.
};

class ArrayQueue {
private:
    //Pointer (*) ile tanımlanır çünkü boyutunu baştan bilmiyoruz.Kullanıcı girince dinamik olarak oluşturacağız.
    ArrayItem* items; // Dinamik dizi
    int capacity;     // Dizinin maksimum boyutu
    int currentSize;  // O an kuyrukta kaç eleman var

public:
    ArrayQueue(int maxCapacity);
    ~ArrayQueue();

    void Insert(int id, int distance);            // Kuyruğa (arkasına/sona) yeni keşfedilen bir yol ekler
    int ExtractMin();                             // Mesafesi EN KISA olanı bul, sil ve ver.
    //Kuyruktaki eleman sayısı kadar kontrol eder.En küçüğü bulur(O(N)).
    void UpdateDistance(int id, int newDistance); // Daha kısa bir alternatif yol bulunursa güncellenir
    bool IsEmpty() const;
    void Clear();
};