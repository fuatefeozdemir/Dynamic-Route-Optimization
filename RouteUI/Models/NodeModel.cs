using System.Drawing; // Rectangle ve Color için

namespace RouteUI.Models
{
    public class NodeModel
    {
        // C++ tarafındaki id ile %100 aynı olmalı
        public int Id { get; set; }

        // Mantıksal koordinatlar (X. sütun, Y. satır)
        public int X { get; set; }
        public int Y { get; set; }

        // Ekranda kapladığı alan (Tıklama kontrolü ve çizim için)
        public Rectangle Bounds { get; set; }

        // C#'ın haritayı boyarken kullanacağı durum bilgisi
        public CellStatus Status { get; set; } = CellStatus.Empty;

        public NodeModel(int id, int x, int y, Rectangle bounds)
        {
            Id = id;
            X = x;
            Y = y;
            Bounds = bounds;
        }
    }
}