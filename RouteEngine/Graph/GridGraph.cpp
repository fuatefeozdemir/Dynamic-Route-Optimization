#include "GridGraph.h"
#include <cstdlib>
#include <ctime>

// Constructor
GridGraph::GridGraph(int _width, int _height) {
    width = _width;
    height = _height;

    // Rastgele sayı üreticiyi saat verisiyle başlatıyoruz (Engeller için)
    srand((unsigned int)time(NULL));

    int totalNodes = width * height; // Kullanıcıdan alınan bilgilerle harita boyutu oluşturulur

    //Hafızada yer ayırıyoruz. tum harıta ıcın (Dinamik Dizi)
    nodes = new Node*[totalNodes];//aslında lınkedlıst de bu kadar acılıyo sonra hepsının ıcını komsularla dolduruyoruz

    //yol cızecıgım komsular ıcın yer
    adjacencyList = new LinkedList[totalNodes];

    // Haritadaki her hücreye sırayla id verilir
    int currentId = 0;
    for (int y = 0; y < height; y++) {           // Satırları geziyoruz (Yukarıdan aşağı)
        for (int x = 0; x < width; x++) {        // Sütunları geziyoruz (Soldan sağa)
            nodes[currentId] = new Node(currentId, x, y);
            currentId++;
        }
    }

    BuildConnections();
    //duvar yıkıp ya da yaparsam yollar eskisi gıbı kalmasın guncellensın diye

}

// Hücre idsini verir
Node* GridGraph::GetNode(int id) {
    // istenen ID harita sınırları içinde mi?
    if (id >= 0 && id < (width * height)) {
        return nodes[id]; // Diziden o hücreyi bul ve gönder
    }
    return nullptr; // Eğer yoksa null döndür
}

// Yıkıcı Metot
GridGraph::~GridGraph() {
    int totalNodes = width * height;

    //her Node'u tek tek siliyoruz
    for (int i = 0; i < totalNodes; i++) {
        delete nodes[i];
    }

    //Sonra bu Node'ları tutan ana listeyi siliyoruz
    delete[] nodes;

    // Komşuluk listesi siliniyor
    delete[] adjacencyList;
}

//KOORDİNAT İLE HÜCRE BULMA
//ıd ıle bulmanın kordınatlı halı
Node* GridGraph::GetNode(int x, int y) {
    // Güvenlik: X ve Y haritanın dışına taşıyor mu?
    if (x >= 0 && x < width && y >= 0 && y < height) {

        // 2 Boyutlu koordinatı (X,Y) 1 Boyutlu dizi ID'sine çevirme formülü
        // x->sutun ,y-> satır ,width-> genişlik(sutun sayisi)
        int id = (y * width) + x;

        return nodes[id];
    }
    return nullptr; //Bilgisayarı çökertmek yerine boş/yok döndürür.
}

int GridGraph::GetTotalNodes() {
    return width * height;
}

LinkedList* GridGraph::GetNeighbors(int id) {//gidebilecegım adreslerı ver
    // Güvenlik kontrolü: ID harita sınırları içinde mi?
    if (id >= 0 && id < (width * height)) {
        return &adjacencyList[id]; // O ID'nin komşuluk listesinin ADRESİNİ gönder.

            // & kullanarak kopyasını değil,kendisininkomşu listesini veriyor.
    }
    return nullptr;
}

void GridGraph::BuildConnections() {//komsu yolları cızen komsu defterı cıkaran
    //linkedlistin içini dolduran
    int totalNodes = width * height;

    for (int i = 0; i < totalNodes; i++) {
        // Önce o hücrenin eski bağlantılarını silelim ki karışıklık olmasın
        adjacencyList[i].Clear();

        Node* currentNode = GetNode(i);

        // Eğer bu kare bir duvarsa, kimseyle bağlantı kuramaz!
        if (currentNode->GetIsObstacle()) continue;

        int x = currentNode->GetCoordinate().X;
        int y = currentNode->GetCoordinate().Y;

        // 4 Yönü kontrol etmek için koordinat değişimleri:
        int dx[] = {0, 0, 1, -1}; // Sağ, Sol değişimleri
        int dy[] = {-1, 1, 0, 0}; // Yukarı, Aşağı değişimleri

        for (int j = 0; j < 4; j++) {
            int neighborX = x + dx[j];
            int neighborY = y + dy[j];

            //Harita sınırları içinde mi?
            if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height) {

                Node* neighborNode = GetNode(neighborX, neighborY);

                // Komşu kare duvar mı?
                if (neighborNode != nullptr && !neighborNode->GetIsObstacle()) {
                    // Her şey yolunda! Yolu deftere yazıyoruz.
                    // (Komşu ID'sini ve gidiş maliyetini 1 olarak ekle)
                    adjacencyList[i].AddEdge(neighborNode->GetId(), 1);
                    //buıltte yolu ve yolun oznıtelıklerını olusturan asıl sey add edge
                }//buılt uygunluk ıcın tarama yapıyo bı nevı
            }
        }
    }
}

//  HÜCREYİ DUVAR YAPMA VEYA DUVARI YIKMA
//Toogle tersini döndürür
void GridGraph::ToggleObstacle(int id) {
    Node* targetNode = GetNode(id);  //id ile o kareyi seciyoruz

    // Eğer kare gerçekten varsa (null değilse)
    if (targetNode != nullptr) {
        // Şu anki durumu al (Duvar mı değil mi ona göre tersi durumuna ceviricez
        bool currentState = targetNode->GetIsObstacle();

        // Tam tersine çevir (Duvar varsa yol yap, yolsa duvar yap)
        targetNode->SetIsObstacle(!currentState);

        // NOT: Buradaki BuildConnections çağrısını sildik.
        // Çünkü art arda 10.000 engel eklendiğinde her seferinde grafı yeniden kurmak sistemi kilitler.
        // Bağlantılar artık işlem bitince topluca kurulacak.
    }
}

// --- YENİ EKLENEN PERFORMANS (TOPLU İŞLEM) FONKSİYONLARI ---

int* GridGraph::GenerateRandomObstacles(int probabilityPercent, int& outCount) {
    int totalNodes = width * height;

    // En kötü ihtimalle tüm hücreler engel olabilir, geçici bir dizi açıyoruz
    int* tempObstacles = new int[totalNodes];
    int count = 0;

    for (int i = 0; i < totalNodes; i++) {
        Node* node = GetNode(i);
        // Sadece boş olan hücrelere rastgele engel koy (Start/End gibi önceden dolu olmayanlar)
        if (node != nullptr && !node->GetIsObstacle()) {

            // Verilen ihtimalle (örneğin %20) rastgele sayı çek
            if ((rand() % 100) < probabilityPercent) {
                node->SetIsObstacle(true);
                tempObstacles[count] = i; // Oluşturulan engelin ID'sini kaydet
                count++;
            }
        }
    }

    outCount = count;

    // Harita bağlantılarını tek tek değil, tüm engeller eklendikten sonra TOPLUCA yeniden kur!
    if (count > 0) {
        BuildConnections();
    }

    // Tam boyutta bir dizi döndür ki C# tarafı Marshal.Copy ile alabilsin
    if (count == 0) {
        delete[] tempObstacles;
        return nullptr;
    }

    int* finalObstacles = new int[count];
    for (int i = 0; i < count; i++) {
        finalObstacles[i] = tempObstacles[i];
    }

    delete[] tempObstacles;
    return finalObstacles; // Geri dönen bu dizi EngineAPI tarafından C#'a iletilecek
}

void GridGraph::ClearAllObstacles() {
    int totalNodes = width * height;
    bool needsRebuild = false;

    for (int i = 0; i < totalNodes; i++) {
        Node* node = GetNode(i);
        if (node != nullptr && node->GetIsObstacle()) {
            node->SetIsObstacle(false); // Duvarı yık
            needsRebuild = true;
        }
    }

    // Eğer silinen bir engel olduysa bağlantıları topluca güncelle
    if (needsRebuild) {
        BuildConnections();
    }
}

void GridGraph::ResetGraph(int newWidth, int newHeight) {
    int oldTotal = width * height;

    // 1. Eski bellekleri temizle
    for (int i = 0; i < oldTotal; i++) {
        delete nodes[i];
    }
    delete[] nodes;
    delete[] adjacencyList;

    // 2. Yeni boyutları ata
    width = newWidth;
    height = newHeight;
    int newTotal = width * height;

    // 3. Yeni bellekleri tahsis et ve haritayı baştan kur
    nodes = new Node*[newTotal];
    adjacencyList = new LinkedList[newTotal];

    int currentId = 0;
    for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {
            nodes[currentId] = new Node(currentId, x, y);
            currentId++;
        }
    }



    BuildConnections();
}

void GridGraph::RemoveObstaclesBatch(int* ids, int count) {
    if (ids == nullptr || count <= 0) return;

    for (int i = 0; i < count; i++) {
        Node* node = GetNode(ids[i]);
        if (node != nullptr && node->GetIsObstacle()) {
            node->SetIsObstacle(false);
        }
    }
    // Tüm engeller kalktıktan sonra bağlantıları topluca ör
    BuildConnections();
}