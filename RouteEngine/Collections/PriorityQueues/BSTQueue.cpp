#include "BSTQueue.h"

// Constructor
BSTQueue::BSTQueue() : root(nullptr) {}
//ınsert gelınce dırekt burası bos deyıp ekleyebılsın dıye
// YIKICI: Bellek sızıntısı olmaması için ağacı temizler
BSTQueue::~BSTQueue() {
    destroyTree(root);
}

// Özyinelemeli temizlik: Önce çocukları sonra babayı siler
void BSTQueue::destroyTree(BSTNode* node) {
    if (node != nullptr) {
        destroyTree(node->left);
        destroyTree(node->right);
        delete node;
    }
}

// EKLEME: Mesafeye bakarak sola veya sağa yerleştirir
void BSTQueue::Insert(int id, double distance) {
    root = insertRecursive(root, id, distance);
}

BSTNode* BSTQueue::insertRecursive(BSTNode* node, int id, double distance) {
    if (node == nullptr) {
        return new BSTNode(id, distance);
    }

    // Küçük mesafeler sola, büyük veya eşitler sağa
    if (distance < node->distance) {
        node->left = insertRecursive(node->left, id, distance);
    } else {
        node->right = insertRecursive(node->right, id, distance);
    }
    return node;
}

// EN KÜÇÜĞÜ ÇIKARMA: "En sola git" kuralı //zaten kucukler sola
int BSTQueue::ExtractMin() {
    if (root == nullptr) return -1;

    BSTNode* parent = nullptr;
    BSTNode* current = root;

    // 1. ADIM: En sola kadar git (En küçük mesafe oradadır)
    while (current->left != nullptr) {
        parent = current;
        current = current->left;
    }

    //2.ADIM: Veriyi (ID) alalım
    int minId = current->id;

    // 3. ADIM: Düğümü ağaçtan koparma
    if (parent == nullptr) {
        // Eğer kök düğümün kendisi en küçükse (solu yoksa)
        // Kökü sağdaki çocuğuna devret
        root = root->right;
    } else {
        // Değilse, babasının sol kolunu, silinenin sağ koluna bağla
        // (Çünkü silinenin solu zaten yok, sağında ne varsa o kalmalı)
        parent->left = current->right;
    }

    delete current; // Hafızayı boşalt
    return minId;
}

bool BSTQueue::IsEmpty() {
    return root == nullptr;
}