#include "GridGraph.h"

// Constructor
GridGraph::GridGraph(int _width, int _height) {
    width = _width;
    height = _height;
    
    int totalNodes = width * height; // Kullanıcıdan alınan bilgilerle harita boyutu oluşturulur

    //Hafızada yer ayırıyoruz. tum harıta ıcın (Dinamik Dizi)
    nodes = new Node*[totalNodes];//aslında lınkedlıst de bu kadar acılıyo sonra hepsının ıcını komsularla dolduruyoruz

    //yol cızecıgım komsular ıcın yer
    adjacencyList = new LinkedList[totalNodes];

    //Haritayı hücrelerle dolduruyoruz. Yanı koca harıtadakı her kucuk kutucuga ıd verıyoruz
    // Anlaşılır ve klasik iç içe for döngüleri kullanıyoruz.
    int currentId = 0;
    for (int y = 0; y < height; y++) {           // Satırları geziyoruz (Yukarıdan aşağı)
        for (int x = 0; x < width; x++) {        // Sütunları geziyoruz (Soldan sağa)

            // Fabrikadan yeni bir Node çıkarıyoruz ve dizimize ekliyoruz.
            nodes[currentId] = new Node(currentId, x, y);
            /*yanı koca boş yer actık sonra o yerın ıcıne
             *tek tek asıl adresler ve ıdlerle doldurduk ondan ıkı tane new yer acma var
             */

            currentId++; // ID'yi bir sonraki kare için artırıyoruz (0, 1, 2... 2499)
        }
    }

    BuildConnections();
    //duvar yıkıp ya da yaparsam yollar eskisi gıbı kalmasın guncellensın diye

}

// KİMLİK NO İLE HÜCRE BULMA
//duvar falan koyarken hucreyı bulmak ıcın
Node* GridGraph::GetNode(int id) {
    // Güvenlik: İstenen ID harita sınırları içinde mi?
    if (id >= 0 && id < (width * height)) {
        return nodes[id]; // Diziden o hücreyi bul ve gönder
    }
    return nullptr; // Eğer hatalı bir ID ise "Boş/Yok" (null) döndür
}
//yıkıcı metot
GridGraph::~GridGraph() {//Ana haritanın her seyini temizliyoruz ramdan
    int totalNodes = width * height;

    //her Node'u tek tek siliyoruz
    for (int i = 0; i < totalNodes; i++) {
        delete nodes[i];
    }

    //Sonra bu Node'ları tutan ana listeyi siliyoruz
    delete[] nodes;

    //komşuluk listesini komple siliyoruz
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

        BuildConnections();
        //yollar eskisi gibi kalmasın(komşuluk listeleri guncellenmeli) diye yeniden harita kurulmalı

    }
}