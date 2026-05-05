//
// Created by Efe on 5.05.2026.
//
//Sığ Öncelikli Arama (BFS - Breadth First Search) saglar
//yani adım adım ılerler komsudan komsuya atlamaz  Stack ile yapılan Derinlik Öncelikli Arama - DFS onune gecer
//butun yapıyı atlaya altaya bosuna dolasmak yerıne en yakındakılerden emın olarak ılerler
#include "Queue.h"

// Kurucu Metot (Başlangıçta front ve rear null)
//ilk acilista kimse yok
Queue::Queue() {
    front = nullptr;
    rear = nullptr;
}

// Yıkıcı Metot
Queue::~Queue() {
    Clear();
}

// Kuyruğun en arkasına eleman ekler
void Queue::Enqueue(int id) {
    QueueNode* newNode = new QueueNode(id);

    if (rear == nullptr) {
        front = rear = newNode;
        return;
    }

    rear->next = newNode;
    rear = newNode;
}

// Kuyruğun başındaki elemanı çıkarır
int Queue::Dequeue() {

    if (IsEmpty()) {
        return -1;
    }
    QueueNode* temp = front;
    int id = temp->data;

    front = front->next;

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