#include "DijkstraSolver.h"
#include "../Core/Timer.h"
#include "../Collections/PriorityQueues/ArrayQueue.h"
#include "../Collections/PriorityQueues/MinHeap.h"
#include "../Collections/PriorityQueues/BSTQueue.h"
#include "../Collections/Stack.h"
#include "../Collections/Queue.h"

const int INFINITY_DIST = 999999;//sonsuz mesafe

//Graf=Harita,başlangıç bitiş ıd,queueType=hangi arama yöntemini kullanayım,etrics& outMetrics=kaç kareye ne kadar süreyle baktığını yazmak için
int* DijkstraSolver::Solve(GridGraph* graph, int startId, int endId, int queueType, Metrics& outMetrics) {
    int totalNodes = graph->GetTotalNodes();

    //HARİTAYI TEMİZLEME
    for (int i = 0; i < totalNodes; i++) {
        Node* node = graph->GetNode(i);
        if (node != nullptr) {
            node->SetDistance(INFINITY_DIST);   //herkesin uzaklığı sonsuz olsun
            node->SetPreviousNodeId(-1);    // buraya nereden geldim bilgisini sil
            node->SetVisited(false); //eski haritadan kalanlari temizlemek için
        }
    }

    //Başlangıç noktasını
    Node* startNode = graph->GetNode(startId);
    if (startNode) startNode->SetDistance(0);   // başladığı karenin maaliyeti 0 dır.

    // Metrikleri başlat
    Timer timer;
    timer.Start();
    // C# ta göstermek için en başta herşeyi sıfırlıyoruz
    int nodesExamined = 0;//adım attığı kareyi tutar
    outMetrics.routeFound = false;//yol bulununca true olcak
    outMetrics.visitedNodes = nullptr; //Başlangıçta boş, ekrana çizme için kullanıyor su an bos hıc bişi çizme
    outMetrics.visitedCount = 0;       //Başlangıçta 0

// baslangıc ile hedef ayni ise kontrolu icin
    if (startId == endId) {
        outMetrics.routeFound = true;
        outMetrics.pathLength = 0;
        outMetrics.timeMicroseconds = 0; // Hiç zaman harcamadık
        return nullptr; // programı bitirir.
    }
    //C# taki hedefe ulaşana kadar gidilebilecek yolların gösterimi için animasyon kuyruğu.
    Queue animationQueue;

    //KUYRUK SEÇİMİ(Performans karşılaştırmaları için verilen 3 veriyapı)
    //herhangi birini kullanabilir seçim için işaretçilerini yazıyo ama henüz yer kaplamıyolar
    ArrayQueue* arrayQueue = nullptr;
    MinHeap* minHeap = nullptr;
    BSTQueue* bstQueue = nullptr;

//sıralamalara seçim için numara veriyoruz
    // C#ta seçilen yapıya göre o veriyapısını dolduruyoruz(Başlangıç noktasını belirtiyoruz)
    if (queueType == 1) {
        //başlangıç nok olan uzaklık
        //fonk bittiği an yok olmasın diye new
        arrayQueue = new ArrayQueue(totalNodes);
        arrayQueue->Insert(startId, 0);
    } else if (queueType == 2) {
        minHeap = new MinHeap(totalNodes);
        minHeap->Push(startId, 0.0f);
    } else if (queueType == 3) {
        bstQueue = new BSTQueue();
        bstQueue->Insert(startId, 0.0);
    }

    //ANA ARAMA DÖNGÜSÜ
    while (true) {
        int currentId = -1;//başlangıçta id olmadığı için(currentid-> sırada gidilecek yol)

        // Seçilen yapıdan en yakın düğümü çek
        if (queueType == 1) {
            if (arrayQueue->IsEmpty())  //boş ise donguden çık
                break;
            currentId = arrayQueue->ExtractMin();   //bu fonksiyon ile gidilebilecek en kısa yolu verir
        } else if (queueType == 2) {
            if (minHeap->IsEmpty())//liste boş mu kontrolü
                break;
            currentId = minHeap->ExtractMin();//minheap de piramidin en başı
        } else if (queueType == 3) {
            if (bstQueue->IsEmpty())
                break;
            currentId = bstQueue->ExtractMin();//en küçüğü seçmek için
        }

        if (currentId == -1)
            break;
/*Dijkstra algoritmasında bir kareyi kuyruktan çıkardığında,
 *ona daha önce bakıp bakmadığını kontrol etmeyince, algoritma aynı kareyi
 *(eğer farklı yollardan tekrar kuyruğa girdiyse) defalarca işler. Bu da performansı düşürür.
 */

        Node* currentNode = graph->GetNode(currentId);
        // Farklı yoolardan aynı kareye tekrar gelindiyse,tekrar bakma
        if (currentNode->IsVisited()) continue; // Döngünün başına dön, sıradaki ID'yi çek

        // Ziyaret edilmemişse bu kareye baktığımızı işaretliyoruz
        currentNode->SetVisited(true);
        // İstatistikleri güncelle (Bu kareye bakıyoruz)
        nodesExamined++; //bi nevi incelenen kare sayacı
        animationQueue.Enqueue(currentId);//bu mavi animasyon için
       //gidilecek mevcut ıd yı kuyruga atar

        //Hedefe ulaştık mı?
        //endID=tıklanan hedef kare
        if (currentId == endId) {
            outMetrics.routeFound = true;//rota bulununca true artık çizebilir
            break;//yolu çizer donguden çıkar
        }
//Once hedef mi diye bakıyoruz kı fazladan adım atmasın o yüzden  komşu bakma sonraki satırda

        int currentDist = currentNode->GetDistance();//burası ustunden oldugum kareyle ılgılenıyo

        // KOMŞULARI GEZME
        LinkedList* neighborsList = graph->GetNeighbors(currentId);
        ListNode* neighborNode = neighborsList->GetHead();//burasıda ustunde oldugum karenin komsu kareleri ile ilgileniyo

        while (neighborNode != nullptr) {//butun komşularını geziyoruz
            //komşularin ozellikleri
            int neighborId = neighborNode->TargetNodeId;
            float weight = neighborNode->Weight;

            Node* neighbor = graph->GetNode(neighborId);

            //Engel kontrolü
            if (neighbor->GetIsObstacle()) {
                neighborNode = neighborNode->Next;  //eğer duvarsa diğer komşuya geç,dongunun başına dön
                continue;
            }
//bu komsıyla beraber mesafe ne kadar
            int newDist = currentDist + (int)weight;

            //Gevşetme (Relaxation)
            //eski mesafe ne kadardı,hangisi daha kisa,buraya nerden geldim ;hepsini yapan yer
            if (newDist < neighbor->GetDistance()) {
                neighbor->SetDistance(newDist);
                neighbor->SetPreviousNodeId(currentId);
    //Maaliyet guncellendiği için veri yapılarının bekleme odasına o komşuyu alıyoruz
                if (queueType == 1) {
                    arrayQueue->Insert(neighborId, newDist);    //arkaya ekler
                } else if (queueType == 2) {
                    if (minHeap->Contains(neighborId)) {//bu komşu zaten içeridemi diye kontrol eder
                        minHeap->DecreaseKey(neighborId, (float)newDist);   //içeride ise decrease ile yolu gunceller
                    } else {
                        minHeap->Push(neighborId, (float)newDist);
                        //yeni daha kısa yol keşfedince listenin başına alıyo
                    }
                } else if (queueType == 3) {
                    bstQueue->Insert(neighborId, (double)newDist);
                }
            }
            neighborNode = neighborNode->Next;  //komşu listemizdeki diğer komşuya geç
        }
    }

    timer.Stop();   //Metrics (C#'a verilecek rapor) doldurulur
    outMetrics.timeMicroseconds = timer.GetMicroseconds();//total zaman
    outMetrics.nodesExamined = nodesExamined;//bakılan kare kayit ,bunlar c# a yazdıırılır

    //Animasyon verisini C#'a göndermek için Metrics dizisine aktarıyoruz
    outMetrics.visitedCount = nodesExamined + (outMetrics.routeFound ? 1 : 0);  //yol bulunduysa +1 ekleniyor(son hedef yol)
    if (outMetrics.visitedCount > 0) {
        outMetrics.visitedNodes = new int[outMetrics.visitedCount];//kuyruktan diziye aktarma
        for (int i = 0; i < outMetrics.visitedCount; i++) {
            outMetrics.visitedNodes[i] = animationQueue.Dequeue();// dequeue ile fıfo mantıgı ile diziye aktarır
        }
    }

    //BELLEK TEMİZLİĞİ VE ROTA OLUŞTURMA
    if (!outMetrics.routeFound) {
        //yol bulunamadıysa yani yol uzunlugu 0 ise temizlik yapiyoruz
        outMetrics.pathLength = 0;  //veriyapılarını temizliyoruz
        if (arrayQueue)
            delete arrayQueue;
        if (minHeap)
            delete minHeap;
        if (bstQueue)
            delete bstQueue;
        return nullptr;
    }


    //Stack ile yolu tersine çevir
    //Burada artık stackın LIFO mantigi ile hedeften baslangica yolu ciziyoruz
    Stack pathStack;
    int stepId = endId;
    int pathCount = 0;
    while (stepId != -1) {
        pathStack.Push(stepId);
        stepId = graph->GetNode(stepId)->GetPreviousNodeId();
        pathCount++;
    }
    /*bu kisim gereksiz yorup 2iş yaptigi icin iptal ettik
//şimdi burada elimizdeki stackı baska bir stacke aktarıyoruz boylece sirasi donmus oluyo
    //her aktarımda da bır adım gibi sayacı arttırıyoruz boylece adım sayısı ortaya cikiyor
    //birde animasyonda tersten göstermesin diye
    int count = 0;
    Stack tempStack;
    while (!pathStack.IsEmpty()) {
        tempStack.Push(pathStack.Pop());
        count++;
    }*/
    // 2. Yolun uzunluğunu alıyoruz

    outMetrics.pathLength = pathCount;
    int* finalPath = new int[pathCount];

    // 3. İkinci Stack (tempStack) kullanmıyoruz!
    // Direkt pathStack'ten Pop yaparak diziyi dolduruyoruz.(LIFO)
    // Stack'e en son giren "Başlangıç" olduğu için, ilk Pop yapıldığında "Başlangıç" çıkar.
    for (int i = 0; i < pathCount; i++) {
        finalPath[i] = pathStack.Pop();
    }

/*
    int* finalPath = new int[count];//count kdar hafıza alani
    for (int i = 0; i < count; i++) {
        //olusturdugumuz yolu takip eden yeni dizi
        finalPath[i] = tempStack.Pop();
    }
    */
//temizlik
    if (arrayQueue)
        delete arrayQueue;
    if (minHeap)
        delete minHeap;
    if (bstQueue)
        delete bstQueue;

    return finalPath;
}