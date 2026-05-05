//
// Created by Efe on 5.05.2026.
//

#include "Set.h"

// Greedy algoritması eklenirse bu sınıfın güncellenmesi gerekecek

// Kurucu Metot
Set::Set(int maxCapacity) {
    capacity = maxCapacity;
    items = new bool[capacity]; // Bellekte kapasite kadar bool değişkeni için yer ayırıyoruz

    // Dizinin içini başlangıçta tamamen false olarak doldurur
    for (int i = 0; i < capacity; i++) {
        items[i] = false;
    }
}

// Yıkıcı Metot
Set::~Set() {
    delete[] items;
}

// Kümeye ID ekleme
void Set::Add(int id) {
    // ID sınırların içinde mi
    if (id >= 0 && id < capacity) {
        items[id] = true;
    }
}

//Kümede bu ID var mi
//zaten gitmis miydim
bool Set::Contains(int id) const {
    if (id >= 0 && id < capacity) {
        return items[id];
    }
    return false; // Harita dışıysa gidilemez
}

// Kümeyi sıfırlama
//yeni haritaya hazirlik
void Set::Clear() {
    for (int i = 0; i < capacity; i++) {
        items[i] = false;
        //constructor da for dongusu ile de yapiyoruz aslinda
        //ama bunu istedigimiz zaman cagirip kullanabiliriz.
    }
}