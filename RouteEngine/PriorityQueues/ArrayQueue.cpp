#include "ArrayQueue.h"

// Kurucu Metot
ArrayQueue::ArrayQueue(int maxCapacity) {
    capacity = maxCapacity;
    currentSize = 0;
    items = new ArrayItem[capacity];
}

// Yıkıcı Metot
ArrayQueue::~ArrayQueue() {
    delete[] items;
}

// Kuyruğa yeni eleman ekleme (Sıranın sonuna ekler)
void ArrayQueue::Insert(int id, int distance) {
    if (currentSize < capacity) {
        items[currentSize].id = id;
        items[currentSize].distance = distance;
        currentSize++;
    }
}

// Performans testinin odak noktası: En kısa mesafeyi bulma (Hızı O(N))
int ArrayQueue::ExtractMin() {
    // Kuyruk boşsa çökmemek için -1 dön
    if (IsEmpty()) return -1;

    // Şimdilik en küçüğün en baştaki eleman olduğunu varsayıyoruz
    int minIndex = 0;
    int minDistance = items[0].distance;

    // Bütün diziyi baştan sona tara ve gerçekten en küçük olanı bul
    // (İşte bu for döngüsü, harita büyüdükçe Dijkstra'yı yavaşlatan asıl nedendir)
    for (int i = 1; i < currentSize; i++) {
        if (items[i].distance < minDistance) {
            minDistance = items[i].distance;
            minIndex = i;
        }
    }

    // Bulunan en kısa mesafeli karenin ID'sini kaydet
    int minId = items[minIndex].id;

    // Hızlı Silme İşlemi (O(1)):
    // Aradaki bir elemanı sildiğimizde dizide boşluk kalmaması gerekir.
    // Bütün elemanları kaydırmak (O(N)) çok yavaş olacağı için,
    // dizinin en sonundaki elemanı, sildiğimiz boşluğa kopyalıyoruz.
    items[minIndex] = items[currentSize - 1];

    // Boyutu 1 azaltarak en sondaki elemanı yok sayıyoruz
    currentSize--;

    return minId;
}

// Dijkstra daha kısa bir yol bulduğunda eski mesafeyi günceller
void ArrayQueue::UpdateDistance(int id, int newDistance) {
    for (int i = 0; i < currentSize; i++) {
        if (items[i].id == id) {
            // Eğer yeni bulunan yol, eski bildiğimizden daha kısaysa güncelle
            if (newDistance < items[i].distance) {
                items[i].distance = newDistance;
            }
            break;
        }
    }
}

// Kuyruk boş mu kontrolü
bool ArrayQueue::IsEmpty() const {
    return currentSize == 0;
}

// Kuyruğu temizleme
void ArrayQueue::Clear() {
    currentSize = 0; // Diziyi bellekten silmeye gerek yok, sayacı sıfırlamak yeterli
}