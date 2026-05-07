#include "MinHeap.h"

// BEKLEME SALONUNUN İNŞASI (Kurucu)
//haritada ne kadar kare varsa o kadar boş bir dizi tutar.
MinHeap::MinHeap(int _capacity) {
    capacity = _capacity;   // toplam kare sayısı
    size = 0; // Başlangıçta salon bomboş
    heapArray = new HeapNode[capacity]; // RAM'den kapasite kadar dizi sırası(boş kutu) alır
}

// SALONUN YIKILMASI (Yıkıcı)
MinHeap::~MinHeap() {
    delete[] heapArray; // o devasa diziyi içindeki herkesle beraber RAM'den silip at
}

//  SALON BOŞ MU KONTROLÜ(Dijkstranın moturunu durduran frendir.) true dönerse arama işlemi biter.
bool MinHeap::IsEmpty() {
    return size == 0;
}

//İKİ DÜĞÜMÜN YERİNİ DEĞİŞTİRME (Gizli Fonksiyon)
void MinHeap::Swap(int index1, int index2) {
    HeapNode temp = heapArray[index1];  // 1.düğümü gecici bri odaya alır
    heapArray[index1] = heapArray[index2];  //2 .düğümü 1 .düğümün boşalan kısmına yerleştirir.
    heapArray[index2] = temp;   // (temp)1. düğümü 2. düğümün olduğu yere yerleştirir.
}


// MİNHEAP'E YENİ BİR DÜĞÜM EKLENMESİ (Push ve Bubble Up)

// Yeni keşfedilen komşuyu kuyruğa ekler.
//Yeni düğüm önce en son çocuk olarak eklenir ve HeapifyUp çağrılarak(küçük maaliyete)göre önde doğru geçirilir.
void MinHeap::Push(int id, float distance) {
    if (size == capacity) return; // Doluysa ekleme

    // 1. Yeni gelen kişiyi dizinin EN SONUNA atar.
    heapArray[size].id = id;
    heapArray[size].distance = distance;
    
    // Yeni eklenen düğümü ebeveynleriyle kıyaslayarak doğru yere çıkartır.
    HeapifyUp(size);
    
    size++; // Salon mevcudunu 1 artır
}
//Yeni gelen düğümü MinHeape göre ebeveynleriyle kıyaslayarak(bir üst düğümleriyle) doğru yere yerleştirir.
void MinHeap::HeapifyUp(int index) {
    int currentIndex = index;   //şuanki durduğu yerin numarası
    int parentIndex = GetParentIndex(currentIndex);//formulle ebeveyninin numarasını getirir

    // DÖNGÜ KURALI: Ben en tepeye (0. koltuğa) ulaşmadıysam VE
    // Benim maliyetim patronumun maliyetinden KÜÇÜKSE(.distance diyerek karşılaştırır çünkü biz toplam maaliyeti en kısa olanı arıyoruz)
    while (currentIndex > 0 && heapArray[currentIndex].distance < heapArray[parentIndex].distance) {
        Swap(currentIndex, parentIndex); // Patronla yer değiştir
        currentIndex = parentIndex;      // Artık yeni yeri değiştiği patronun numarası
        parentIndex = GetParentIndex(currentIndex); // Yeniden formülü çalıştır, bir üstteki YENİ patronumu bul
        // Patronu yeni düğümden küçük (veya eşit) olana kadar bu değişme işlemi devam eder.
    }
}


// DİJKSTRANIN (KURYENİN) EN KÜÇÜĞÜ ALMASI (ExtractMin ve Sink Down)

// piramidin en tepesindeki(en küçük maaliyetli) alır dijkstaraya verir ve silir.
// Dijkstra'nın her döngüsünde "Bir sonraki durağım neresi?" sorusunun cevabını veren ana butondur.
int MinHeap::ExtractMin() {
    if (size == 0) return -1; // Salon boşsa hata döndür

    // Dijkstraya verilecek olan EN TEPEDEKİ (Kök) kişinin ID'sini kaydet
    int minId = heapArray[0].id; 

    // En sondaki düğümü al, temizle ve köke yaz
    heapArray[0] = heapArray[size - 1];
    size--; // Dijkstraya kök verdiğimiz için mevcudu 1 azaltır.

    // Tepedeki (sondan aldığımız düğümü) kendi dengini bulana kadar aşağı kaydırır
    HeapifyDown(0);

    return minId; //En baştaki(kökte) küçük maaliyetli düğümü dondurur.
}

// Köke sahte bir şekilde oturan büyük maliyetli düğümü, kendinden küçük çocuklarıyla yer değiştirerek aşağı indirir.
void MinHeap::HeapifyDown(int index) {
    int currentIndex = index;   //tepeye yeni konulan düğümün indeksi.

    while (true) {  //kendi yerini bulana kadar dongu devam eder.
        int leftChild = GetLeftChildIndex(currentIndex);    //sol çocuğunun numarasını verir
        int rightChild = GetRightChildIndex(currentIndex);  //sağ çocugunun numarasını verir(karşılaştırma için)
        int smallest = currentIndex; // Şimdilik en küçük tepedeki düğüm diyelim

        // Sol çocuğu var mı? Ve ondan daha mı küçük?
        if (leftChild < size && heapArray[leftChild].distance < heapArray[smallest].distance) {
            smallest = leftChild;
        }

        // Sağ çocuğu var mı? Ve o, sol kardeşinden de mi küçük?
        if (rightChild < size && heapArray[rightChild].distance < heapArray[smallest].distance) {
            smallest = rightChild;  //soldakinden de kü.ükse en küçük sağ komşu yapar
        }

        // Eğer en küçük hala o düğümse (Çocuklarından küçük), demek ki doğru yerdedir
        if (smallest == currentIndex) {
            break;  //döngüyü durdurur
        }

        // Değilse, en küçük çocuğu ile yer değiştirir ve aşağı inmeye devam eder
        Swap(currentIndex, smallest);   //küçük olan çocukla yer değiştirir
        currentIndex = smallest;    // artık yeni yeri o küçük çocuğun yeri oldu
    }
}