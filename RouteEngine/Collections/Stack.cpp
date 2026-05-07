#include "Stack.h"

// Constructor
Stack::Stack() {
    top = nullptr; // Başlangıçta boştur
}

// Yıkıcı Metot
Stack::~Stack() {
    Clear();
}

// Yığına yeni eleman ekleme
void Stack::Push(int id) {
    StackNode* newNode = new StackNode(id);
    
    // Yeni gelen elemanın altını, şu anki en üst elemana bağla
    newNode->next = top;
    
    // Artık yığının en üstü bu yeni eleman oldu
    top = newNode;
}

// Yığından en üstteki (en son eklenen) elemanı çıkarma
int Stack::Pop() {
    // Yığın boşsa -1 döndür
    if (IsEmpty()) {
        return -1;
    }

    // En üstteki elemanı geçici bir pointer'a al
    StackNode* temp = top;
    int id = temp->data;

    // Yığının zirvesini bir alttaki elemana kaydır
    top = top->next;

    // RAM'den eski tepeyi sil (Memory leak önlemi)
    delete temp;

    return id;
}

// Sadece en üstte ne olduğuna bakmak için (Çıkarma yapmaz)
int Stack::Peek() {
    if (IsEmpty()) {
        return -1;
    }
    return top->data;
}

// Yığın boş mu kontrolü
bool Stack::IsEmpty() const {
    return top == nullptr;
}

// Tüm yığını temizleme
void Stack::Clear() {
    while (!IsEmpty()) {
        Pop();
    }
}