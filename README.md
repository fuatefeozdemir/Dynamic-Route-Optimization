# Dynamic Route Optimization

Bu proje, bir başlangıç noktasından hedef noktasına giden en kısa yolu bulurken, arka planda çalışan farklı veri yapılarının performanslarını karşılaştırmayı amaçlar. Temel hedef; rota hesaplama süreçlerinde kullanılan **Dizi (Array)**, **İkili Arama Ağacı (BST)** ve **Min-Heap** veri yapılarının hız ve verimlilik açısından nasıl farklılık gösterdiğini test etmek ve bu sonuçları anlık olarak görselleştirmektir.

## 📁 Proje Mimarisi

Performans ölçümlerini sağlıklı yapabilmek ve arayüzü bu yükten izole etmek için proje iki ana modüle ayrılmıştır:

* **`RouteEngine/` (C++)**: Veri yapılarının ve algoritmaların çalıştığı arka plan motorudur. Performans testleri, süre hesaplamaları ve optimizasyon işlemleri bu izole katmanda gerçekleştirilir.
* **`RouteUI/` (C# - Windows Forms)**: C++ motorundan elde edilen performans metriklerini ve rota algoritmalarının çalışma adımlarını kullanıcıya görsel olarak sunan ön yüz modülüdür.
* **`DynamicRoute.slnx`**: Birbirinden bağımsız çalışan bu iki modülü tek çatı altında bağlayan ve yöneten Visual Studio çözüm dosyasıdır.

## 🧠 Algoritma Mantığı

* **Dijkstra Algoritması:** İki düğüm arasındaki en kısa yolu bulmak için kullanılır ve duraklar arası her bir rota segmentini hesaplar.

## 🏗️ Geliştirilen Veri Yapıları

Motor tarafında projeye özel olarak kodlanan veri yapıları ve görevleri şunlardır:

* **Graph & Linked List:** Izgara haritasının ve her bir karenin geçilebilir komşularını tutan temel iskelettir.
* **Performans Senaryoları:** Dijkstra algoritmasının arama maliyetini test etmek için sırasıyla **Array** (tüm indeksleri tarar), **BST** (mesafeleri sıralı tutar) ve **Min-Heap** (en kısa mesafeyi tepede tutar) kullanılır.
* **Queue:** Algoritmanın yol ararken ziyaret ettiği kareleri kaydederek arayüzdeki animasyonun (FIFO mantığıyla) çizilmesini sağlar.
* **Stack:** Kullanıcının harita üzerindeki adımlarını (LIFO mantığıyla) hafızada tutarak "Geri Al" mekanizmasını yönetir.
* **Set:** Algoritmanın sonsuz döngüye girmesini önler ve ulaşılan durakları hedef havuzundan çıkarır.

## 🛠️ Kullanılan Teknolojiler ve Araçlar

* **Çekirdek Motor (Backend):** C++
* **Kullanıcı Arayüzü (Frontend):** C# (.NET), Windows Forms
* **Geliştirme Ortamları (IDE):** 
  * **Visual Studio 2026**
  * **CLion**
