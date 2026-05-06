#include "GridGraph.h"

//KURUCU METOT: Haritayı inşa ediyoruz
GridGraph::GridGraph(int _width, int _height) {
    width = _width;
    height = _height;
    
    int toplamKareSayisi = width * height; //dısardan gelen harita sınırlarını yazıyoruz

    //Hafızada yer ayırıyoruz. tum harıta ıcın (Dinamik Dizi)
    nodes = new Node*[toplamKareSayisi];//aslında lınkedlıst de bu kadar acılıyo sonra hepsının ıcını komsularla dolduruyoruz

    //yol cızecıgım komsular ıcın yer
    adjacencyList = new LinkedList[toplamKareSayisi];

    //Haritayı hücrelerle dolduruyoruz. Yanı koca harıtadakı her kucuk kutucuga ıd verıyoruz
    // Anlaşılır ve klasik iç içe for döngüleri kullanıyoruz.
    int currentId = 0;
    for (int y = 0; y < height; y++) {           // Satırları geziyoruz (Yukarıdan aşağı)
        for (int x = 0; x < width; x++) {        // Sütunları geziyoruz (Soldan sağa)
            
            // Fabrikadan yeni bir Node çıkarıyoruz ve dizimize ekliyoruz.
            nodes[currentId] = new Node(currentId, x, y);
            /*yanı koca bo yer actık sonra o yerın ıcıne
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

GridGraph::~GridGraph() {//Ana haritanın her seyini temizliyoruz ramdan
    int toplamKare = width * height;

    //her Node'u tek tek siliyoruz
    for (int i = 0; i < toplamKare; i++) {
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
        
        // 2 Boyutlu koordinatı (X,Y) 1 Boyutlu dizi ID'sine çevirme formülü!
        int id = (y * width) + x; 
        
        return nodes[id];
    }
    return nullptr;
}


LinkedList* GridGraph::GetNeighbors(int id) {//gidebilecegım adreslerı ver
    // Güvenlik kontrolü: ID harita sınırları içinde mi?
    if (id >= 0 && id < (width * height)) {
        return &adjacencyList[id]; // O ID'nin komşuluk listesinin ADRESİNİ gönder
    }
    return nullptr;
}
void GridGraph::BuildConnections() {//komsu yolları cızen komsu defterı cıkaran
    //linkedlistin içini dolduran
    int toplamKare = width * height;

    for (int i = 0; i < toplamKare; i++) {
        // Önce o hücrenin eski bağlantılarını silelim ki karışıklık olmasın
        adjacencyList[i].Clear();

        Node* suankiKare = GetNode(i);

        // Eğer bu kare bir duvarsa, kimseyle bağlantı kuramaz!
        if (suankiKare->GetIsObstacle()) continue;

        int x = suankiKare->GetCoordinate().X;
        int y = suankiKare->GetCoordinate().Y;

        // 4 Yönü kontrol etmek için koordinat değişimleri:
        int dx[] = {0, 0, 1, -1}; // Sağ, Sol değişimleri
        int dy[] = {-1, 1, 0, 0}; // Yukarı, Aşağı değişimleri

        for (int j = 0; j < 4; j++) {
            int komsuX = x + dx[j];
            int komsuY = y + dy[j];

            //Harita sınırları içinde mi?
            if (komsuX >= 0 && komsuX < width && komsuY >= 0 && komsuY < height) {

                Node* komsuKare = GetNode(komsuX, komsuY);

                // Komşu kare duvar mı?
                if (komsuKare != nullptr && !komsuKare->GetIsObstacle()) {
                    // Her şey yolunda! Yolu deftere yazıyoruz.
                    // (Komşu ID'sini ve gidiş maliyetini 1 olarak ekle)
                    adjacencyList[i].AddEdge(komsuKare->GetId(), 1);
                    //buıltte yolu ve yolun oznıtelıklerını olusturan asıl sey add edge
                }//buılt uygunluk ıcın tarama yapıyo bı nevı
            }
        }
    }
}
//  HÜCREYİ DUVAR YAPMA VEYA DUVARI YIKMA
void GridGraph::ToggleObstacle(int id) {
    Node* hedefKare = GetNode(id);

    // Eğer kare gerçekten varsa (null değilse)
    if (hedefKare != nullptr) {
        // Şu anki durumu al (Duvar mı değil mi?)
        bool suAnkiDurum = hedefKare->GetIsObstacle();

        // Tam tersine çevir (Du varsa yol yap, yolsa duvar yap)
        hedefKare->SetIsObstacle(!suAnkiDurum);

        BuildConnections();
        //duvar yıkıp ya da yaparsam yollar eskisi gıbı kalmasın guncellensın diye

    }
}

