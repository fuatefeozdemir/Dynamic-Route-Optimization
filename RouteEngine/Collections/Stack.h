#pragma once

// Yığındaki her bir elemanı temsil eden yapı
struct StackNode {
    int data;          // Haritadaki karenin ID'si
    StackNode* next;   // Bir alttaki elemana işaretçi

    // Kurucu metot
    StackNode(int val) : data(val), next(nullptr) {}
};

class Stack {
private:
    StackNode* top; // Yığının en üstündeki eleman (En son eklenen)

public:
    Stack();
    ~Stack();

    void Push(int id);     // Yığının en üstüne eleman ekle
    int Pop();             // Yığının en üstündeki elemanı çıkar ve ver
    int Peek();            // En üstteki elemanı çıkarmadan sadece oku
    bool IsEmpty() const;  // Yığın boş mu?
    void Clear();          // Yığını tamamen boşalt
};