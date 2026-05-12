#include "LinkedList.h"
// Constructor (ilk açıldığında boştur)
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
    ListNode* newNode = new ListNode();

    // Halkanın içine komşu bilgilerini yazıyoruz.
    newNode->TargetNodeId = targetId;
    newNode->Weight = weight;

    // Yeni halkayı listenin en başına bağlıyoruz.
    // Yeni halkanın 'next'i, şu an listenin başında ne varsa orayı göstersin.
    newNode->Next = head;

    head = newNode;
}
// 3.Ekleme İşlemi
void LinkedList::Insert(int targetId, float weight) {
    ListNode* newNode = new ListNode();
    newNode->TargetNodeId = targetId;
    newNode->Weight = weight;
    newNode->Next = nullptr;

    // Eğer liste boşsa head yap
    if (head == nullptr) {
        head = newNode;
        return; //İşimiz bitti, fonksiyondan çık
    }

    ListNode* current = head;
    while (current->Next != nullptr) {
        current = current->Next; //Düğümleri tek tek gez
    }
    current->Next = newNode; //Kuyruk bulundu
}

//4.Arama / Okuma İşlemi
//private yapmıstı .h de onu okuyabılsın dıye  prıvate olmasına ragmen
//sadece ılk vagonun adresinı alıyo bi problem cıkıyo dısardan mudahele edılemıyo
ListNode* LinkedList::GetHead() {
    return head;
}