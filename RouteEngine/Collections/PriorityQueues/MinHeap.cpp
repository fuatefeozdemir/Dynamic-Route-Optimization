#include "MinHeap.h"

// BEKLEME SALONUNUN İNŞASI (Kurucu)
//haritada ne kadar kare varsa o kadar boş bir dizi tutar.
MinHeap::MinHeap(int _capacity) {
    capacity = _capacity;   // toplam kare sayısı
    size = 0; // Başlangıçta salon bomboş
    heapArray = new HeapNode[capacity]; // RAM'den kapasite kadar dizi sırası(boş kutu) alır

    nodeIndices = new int[capacity];    //bu komşuya bakıldımı diye kontrol için bir dizi oluşturuluyor her kare için.
    for (int i = 0; i < capacity; i++) {
        nodeIndices[i] = -1;    // başta hepsine -1 değeri veriyoruz(o komşuya gidilmemiş)
    }
}

// Yıkıcı fonksiyon
MinHeap::~MinHeap() {
    delete[] heapArray; // Minheap için kurulan diziyi siler.
    delete[] nodeIndices;   //ziyaret edildimi listesini siler.
}

// Gidilecek yol,komşu var mı diye kontrol eder.True dönerse arama işlemi biter.
bool MinHeap::IsEmpty() {
    return size == 0;
}

bool MinHeap::Contains(int id) {
    return nodeIndices[id] != -1;   //Bakılan id'deki yola gidildimi diye kontrol eder.
}

void MinHeap::DecreaseKey(int id, float newDistance) {
    int index = nodeIndices[id];    //id si verilerek hangi indekste olduğu yazılır
    //Dijktra kontrol eder o yola gidildimi diye,eğer gidildiyse id sini alır.
    if (newDistance < heapArray[index].distance) {  //Bulunan yeni yolun mesafesi ,eski yoldaki maaliyetten küçükse
        heapArray[index].distance = newDistance;    //daha kısa ise,o yolun mesafesini en kısa olan olarak guncellenir
        HeapifyUp(index);   //Minheap yapısında doğru yere yerleştirilir.
    }
}

//İKİ DÜĞÜMÜN YERİNİ DEĞİŞTİRME
void MinHeap::Swap(int index1, int index2) {
    // [heapArray[index1].id] bu ifade ile index1'in nodeIndıces dizisinde kaçıncı indekste onu veritor
    nodeIndices[heapArray[index1].id] = index2; //  İndex1'in nodeIndıces dizisindeki indeks numarası indeks2 olarak değiştiriliyor
    nodeIndices[heapArray[index2].id] = index1;

    HeapNode temp = heapArray[index1];  // 1.düğümü gecici bri odaya alır(kaybetmemek için)
    heapArray[index1] = heapArray[index2];  //2 .düğümü 1 .düğümün boşalan kısmına üstüne yazılır.
    heapArray[index2] = temp;   // (temp) 1. düğümü 2. düğümün olduğu yere yerleştirir.
}


// MİNHEAP'E YENİ BİR DÜĞÜM EKLENMESİ (Push ve Bubble Up)

// Yeni keşfedilen komşuyu kuyruğa ekler.
//Yeni düğüm önce en son çocuk olarak eklenir ve HeapifyUp çağrılarak(küçük maaliyete)göre önde doğru geçirilir.
void MinHeap::Push(int id, float distance) {
    if (size == capacity) return; // Doluysa ekleme

    // Yeni gelen kişiyi dizinin EN SONUNA atar.
    heapArray[size].id = id;
    heapArray[size].distance = distance;

    nodeIndices[id] = size; // nodeIndıces dizisine eklenen yolun id si (MinHeap teki gibi)verilerek diziye ekleniyor ve içindeki bilgide kaçıncı indekste olduğu.

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
// Dijkstra verilen ekısa yoldan devam ederek komşularına bakar ve bekleme salonuna ekletir.
int MinHeap::ExtractMin() {
    if (size == 0) return -1; // Salon boşsa hata döndür

    // Dijkstraya verilecek olan EN TEPEDEKİ (Kök) kişinin ID'sini kaydet
    int minId = heapArray[0].id;

    nodeIndices[minId] = -1;

    // En sondaki düğümü al, temizle ve köke yaz
    heapArray[0] = heapArray[size - 1];
    size--; // Dijkstraya kök verdiğimiz için mevcudu 1 azaltır.

    if (size > 0) {
        nodeIndices[heapArray[0].id] = 0;   //Sondan alınıp başa eklenen elemanın id'sini alıve nodeındıces dizisinde indeks nosu 0 olarak yazar.
        // Tepedeki (sondan aldığımız düğümü) kendi dengini bulana kadar aşağı kaydırır
        HeapifyDown(0);
    }

    return minId; //En baştaki(kökte) küçük maaliyetli düğümü dondurur.
}

// Köke sahte bir şekilde oturan büyük maliyetli düğümü, kendinden küçük çocuklarıyla yer değiştirerek aşağı indirir.
void MinHeap::HeapifyDown(int index) {
    int currentIndex = index;   //tepeye yeni konulan düğümün indeksi.

    while (true) {  //kendi yerini bulana kadar dongu devam eder.
        int leftChild = GetLeftChildIndex(currentIndex);    //sol çocuğunun numarasını verir
        int rightChild = GetRightChildIndex(currentIndex);  //sağ çocugunun numarasını verir(karşılaştırma için)
        int smallest = currentIndex; // Şimdilik en küçük tepedeki düğüm diyelim

        //Çocugun olup olmadığını anlamak için:formulun ürettiği sayının ,bekleme salonundaki (size) sayıdan küçük olmasına bakmak
        // Sol çocuğu var mı?(Boş odaya bakmayalım) Ve ondan daha mı küçük?
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