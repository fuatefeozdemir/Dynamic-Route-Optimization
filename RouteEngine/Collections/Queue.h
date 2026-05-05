//
// Created by Efe on 5.05.2026.
//
//bu dosyayi bi  kere okuduysa bi daha tekrar tekrar okumasin diye
#ifndef ROUTEENGINE_QUEUE_H
#define ROUTEENGINE_QUEUE_H


#pragma once

// Kuyruktaki her bir düğümün yapısı
struct QueueNode {
  /*önce Node olarak doğar, sonra LinkedList'in içinde bir hedef olarak okunur,
en son da sırası gelene kadar beklemesi için Queue'nun içine data olarak atılır.
   */
    int data;//bu da yine linkedlist TargetNodeId ve Node id nin aynisi
    QueueNode* next;

    // Kurucu metot (Yeni bir yapı oluşturulduğunda çağrılır)
    QueueNode(int val) : data(val), next(nullptr) {}//hızlı yazımı
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
