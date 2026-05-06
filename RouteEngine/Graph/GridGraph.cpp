#include "GridGraph.h"

// ==========================================
// ZEYNEP
// ==========================================

// KURUCU METOT: Haritayı inşa ediyoruz
GridGraph::GridGraph(int _width, int _height) {
    width = _width;
    height = _height;
    
    int toplamKareSayisi = width * height; // Örn: 50 * 50 = 2500

    // Hafızada 2500 kutuluk yer ayırıyoruz. (Dinamik Dizi)
    nodes = new Node*[toplamKareSayisi];

    // (Sude buraya gelip kendi 'adjacencyList' dizisi için hafıza açacak)
    adjacencyList = nullptr; 

    // Haritayı hücrelerle dolduruyoruz.
    // Anlaşılır ve klasik iç içe for döngüleri kullanıyoruz.
    int currentId = 0;
    for (int y = 0; y < height; y++) {           // Satırları geziyoruz (Yukarıdan aşağı)
        for (int x = 0; x < width; x++) {        // Sütunları geziyoruz (Soldan sağa)
            
            // Fabrikadan yeni bir Node çıkarıyoruz ve dizimize ekliyoruz.
            nodes[currentId] = new Node(currentId, x, y);
            
            currentId++; // ID'yi bir sonraki kare için artırıyoruz (0, 1, 2... 2499)
        }
    }
}

// KİMLİK NO İLE HÜCRE BULMA
Node* GridGraph::GetNode(int id) {
    // Güvenlik: İstenen ID harita sınırları içinde mi?
    if (id >= 0 && id < (width * height)) {
        return nodes[id]; // Diziden o hücreyi bul ve gönder
    }
    return nullptr; // Eğer hatalı bir ID ise "Boş/Yok" (null) döndür
}

// KOORDİNAT İLE HÜCRE BULMA (En Kritik Matematik!)
Node* GridGraph::GetNode(int x, int y) {
    // Güvenlik: X ve Y haritanın dışına taşıyor mu?
    if (x >= 0 && x < width && y >= 0 && y < height) {
        
        // 2 Boyutlu koordinatı (X,Y) 1 Boyutlu dizi ID'sine çevirme formülü!
        int id = (y * width) + x; 
        
        return nodes[id];
    }
    return nullptr;
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
    }
}


// ==========================================
// SUDE
// ==========================================

GridGraph::~GridGraph() {
    // Dev 2 buraya program kapanırken RAM'i temizleme kodlarını yazacak...
}

void GridGraph::BuildConnections() {
    // Dev 2 buraya tüm komşuları birbirine bağlayan döngüleri yazacak...
}

LinkedList* GridGraph::GetNeighbors(int id) {
    // Dev 2 buraya o ID'nin bağlı listesini döndüren kodu yazacak...
    return nullptr;
}