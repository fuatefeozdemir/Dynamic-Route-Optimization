//
// Created by Efe on 5.05.2026.
//

#ifndef ROUTEENGINE_QUEUE_H
#define ROUTEENGINE_QUEUE_H


#pragma once

// Kuyruktaki her bir düğümün yapısı
struct QueueNode {
    int data;
    QueueNode* next;

    // Kurucu metot (Yeni bir yapı oluşturulduğunda çağrılır)
    QueueNode(int val) : data(val), next(nullptr) {}
};

class Queue {
private:
    QueueNode* front; // Kuyruğun başı
    QueueNode* rear;  // Kuyruğun sonu

public:
    Queue();
    ~Queue();

    void Enqueue(int id);
    int Dequeue();
    bool IsEmpty() const;
    void Clear();
};


#endif //ROUTEENGINE_QUEUE_H
