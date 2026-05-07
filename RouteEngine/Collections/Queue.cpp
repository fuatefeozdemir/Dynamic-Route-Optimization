//
// Created by Efe on 5.05.2026.
//
//Sığ Öncelikli Arama (BFS - Breadth First Search) saglar
//yani adım adım ılerler komsudan komsuya atlamaz  Stack ile yapılan Derinlik Öncelikli Arama - DFS onune gecer
//butun yapıyı atlaya altaya bosuna dolasmak yerıne en yakındakılerden emın olarak ılerler
#include "Queue.h"

// Constructor (Başlangıçta front ve rear null)
Queue::Queue() {
    front = nullptr;
    rear = nullptr;
}

// Yıkıcı Metot
Queue::~Queue() {
    Clear();
}

// Kuyruğun en arkasına eleman ekleyen fonksiyon
void Queue::Enqueue(int id) {
    QueueNode* newNode = new QueueNode(id);

    // Eğer kuyruk boşsa eklenen düğüm hem baş hem kuyruktur
    if (rear == nullptr) {
        front = rear = newNode;
        return;
    }

    // Eğer kuyruk boşsa kuyruğun sonuna eklenir ve eklenen düğüm kuyruk olur
    rear->next = newNode;
    rear = newNode;
}

// Kuyruğun başındaki elemanı çıkaran fonksiyon
int Queue::Dequeue() {

    // Kuyruk boşsa çıkar
    if (IsEmpty()) {
        return -1;
    }

    QueueNode* temp = front;
    int id = temp->data;

    front = front->next;

    // Eğer kuyruktan eleman çıkarıldığında tamamen boşalıyorsa kuyruğu da null yapar
    if (front == nullptr) {
        rear = nullptr;
    }

    delete temp;

    return id;
}

// Kuyruk boş mu
bool Queue::IsEmpty() const {
    return front == nullptr;
}

// Kuyruğu temizle
void Queue::Clear() {
    while (!IsEmpty()) {
        Dequeue();
    }
}