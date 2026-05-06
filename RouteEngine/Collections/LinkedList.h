#pragma once//her bu dosyaya ihtiyaci oldugunda tekrar tekrar aynı dosyayi cekmek yerine
//ilk ogrendiginden yararlansin her seferinde diye

//list yapisinin icerigini belirliyo
struct ListNode {//her komsu icin bu paketten 1 adet uretiliyo
    int TargetNodeId; //Gitmek istedigimiz komşunun ID'si(verisi)
    float Weight;     //Oraya gitmenin maliyeti yani 5 adım da daha zor bi yoldan ya da 10 adimda daha kolay bi yoldan
    //gidebilir bu maliyet (bizim ızgaramızda 1 birim maliyet olarak gecicek)
    ListNode* Next;   //Bir sonraki olusturulacsk komsunun adresini gösteren işaretçi (Pointer)
};
//typedef yazmaya gerek yok kullanım kisaltmasi icin zaten direkt ceviriyo c++

/* grafin komsu belirlerken kullandigi matematik
 Sağ Komşu: ID + 1 (15 + 1 = 16)

 Sol Komşu: ID - 1 (15 - 1 = 14)

 Alt Komşu: ID + Harita Genişliği (15 + 10 = 25)

 Üst Komşu: ID - Harita Genişliği (15 - 10 = 5)
 (engel kontrolu yaptiktan sonra tabi)
*/
class LinkedList {
private://encapsulatıon (sadece linked list deki metotları degıstırebilir)
    ListNode* head; //Listenin başlangıç noktası(head)

public:// dısardan degisim sadece burdakı parcalarla yapilabilir bunlara erisim var
    //Kurucu ve Yıkıcı Metotlar
    LinkedList();//her harita olusumunda otomatik baslar
    ~LinkedList();//~yikici oldugunu gosteriyo bitirir

    //Temel İşlevler
    void AddEdge(int targetId, int weight);//graph için gereken
    void Insert(int targetId, float weight); //su numaralı yenı komsuyu ekle dıyen fonk
    // LinkedList.h içinde olması gereken:
    void Clear();
    ListNode* GetHead(); //Djikstra ekleyecegi yeri bilsin diye headi verir
};