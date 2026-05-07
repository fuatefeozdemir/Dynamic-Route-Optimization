#include "LinkedList.h"
// 1. Kurucu (İstasyon ilk açıldığında tren boştur)
LinkedList::LinkedList() {
    head = nullptr;
}
void LinkedList::Clear() {
    ListNode* current = head;
    while (current != nullptr) {
        ListNode* next = current->Next;
        delete current;
        current = next;
    }
    head = nullptr; // Listeyi boşa çıkar
}

LinkedList::~LinkedList() {
    //:: bu kapsam cozunurluk oprt. bu sınıfın bu metoduna yazıyorum demek
    ListNode* current = head;
    while (current != nullptr) {
        //boslukta mıyız degıl mıyız
        //vagon direkt silersek next de kaybolur yedekliyoruz
        ListNode* silinecek = current;
        current = current->Next; // Bir sonrakine geç
        delete silinecek;        //Arkada kalanı RAM'den kalıcı olarak sil!
    }
    head = nullptr;
}
void LinkedList::AddEdge(int targetId, int weight) {
    //Hafızadan yeni bir "Halka" (ListNode) siparişi veriyoruz.
    ListNode* newNode = new ListNode();

    //Halkanın içine komşu bilgilerini yazıyoruz.
    newNode->TargetNodeId = targetId;
    newNode->Weight = weight;

    //Yeni halkayı listenin en başına bağlıyoruz.
    // Yeni halkanın 'next'i, şu an listenin başında ne varsa orayı göstersin.
    newNode->Next = head;

    head = newNode;
}
//3.Ekleme İşlemi (Vagonları üretip birbirine bağlama)
void LinkedList::Insert(int targetId, float weight) {
    //Yeni vagonu RAM'de üret (new komutu ile)
    ListNode* newNode = new ListNode();
    newNode->TargetNodeId = targetId;
    newNode->Weight = weight;
    newNode->Next = nullptr; //Yeni vagonun arkası şimdilik boş

    //Eğer tren tamamen boşsa, ilk vagon (lokomotif) bu olsun
    if (head == nullptr) {
        head = newNode;
        return; //İşimiz bitti, fonksiyondan çık
    }

    //Tren boş değilse, en sona kadar git ve oraya tak
    ListNode* current = head;
    while (current->Next != nullptr) {
        current = current->Next; //Vagonları tek tek gez
    }
    current->Next = newNode; //Son vagonu bulduk, yeni vagonu kancaya tak!
}

//4.Arama / Okuma İşlemi
//private yapmıstı .h de onu okuyabılsın dıye  prıvate olmasına ragmen
//sadece ılk vagonun adresinı alıyo bi problem cıkıyo dısardan mudahele edılemıyo
ListNode* LinkedList::GetHead() {
    return head;
}