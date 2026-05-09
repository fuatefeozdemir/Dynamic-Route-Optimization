#pragma once

// Min-Heap piramidinde tutacağımız her bir Kargo Paketi
struct HeapNode {
    int id;          // Kuryenin gideceği hedefin kapı numarası (Node ID)
    float distance;  // Başlangıçtan bu hedefe olan TOPLAM maliyet (mesafe)
};
// Her katmanda ebeveyn çocuklardan daha küçük olmak zorundadır(kök en küçüktür)
// Çocuklar arasında bir büyüklük/küçüklük kuralı yoktur.
class MinHeap {
private:
    // Ağacı tutacağımız DİZİ (Evet, ağaç çizeceğiz ama bunu gizlice bir diziyle yapacağız!)
    HeapNode* heapArray;

    int capacity; // (Haritadaki toplam kare sayısı)
    int size;     // Şu an bekleme salonunda kaç kare var?(toplam kaç kişi var salonda)

    // --- SİHİRLİ MATEMATİK (Diziyi Ağaç Gibi Kullanma Formülleri) ---
    // Bir düğümün indeksini verip, ailesinin indeksini bulduğumuz formüller
    int GetParentIndex(int index) { return (index - 1) / 2; }
    int GetLeftChildIndex(int index) { return (2 * index) + 1; }
    int GetRightChildIndex(int index) { return (2 * index) + 2; }

    //  GİZLİ KURALLAR (Private)
    void Swap(int index1, int index2); // İki işçinin yerini değiştirir
    void HeapifyUp(int index);         // Yeni eklenen düğümü üstteki düğüm ile karşılaştırıp dopru yere koyma (Bubble Up)
    void HeapifyDown(int index);       // Tepede olan düğümü aldıktan sonra en sonki çocuğu tepeye koyar ve kaydırma işlemi yapar. (Sink Down)

public:
    // Kurucu ve Yıkıcı
    MinHeap(int _capacity); // Oyun açıldığında kurye için bekleme salonunu inşa eder
    ~MinHeap();             // Oyun kapandığında salonu yıkar (RAM'den temizler)

    // DIŞ DÜNYANIN (Dijkstra'nın) KULLANACAĞI FONKSİYONLAR
    void Push(int id, float distance); // Yeni komşu bulduğunda, id sini ve maaliyetini ekler.(HeapifyUp çalışır ve paketi doğru yere yerleştirir)
    int ExtractMin();                  // En düşük maliyetli kareyi (Tepedekini) ver ve tepeyi siler(HeapifyDown ile tekrar düzenler.)
    bool IsEmpty();                    // Salon boş mu? (Kuryenin gidecek yeri kalmadıysa döngüyü bitirir)
};