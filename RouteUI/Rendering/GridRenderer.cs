using System.Drawing;
using RouteUI.Models;
using RouteUI.Logic;

namespace RouteUI.Rendering
{
    public static class GridRenderer
    {
        private static readonly Brush EmptyBrush = Brushes.White;
        private static readonly Brush ObstacleBrush = Brushes.FromArgb(45, 45, 45); // Koyu Gri
        private static readonly Brush StartBrush = Brushes.LimeGreen;              // Başlangıç (Yeşil)
        private static readonly Brush EndBrush = Brushes.Crimson;                // Bitiş (Kırmızı)
        private static readonly Brush PathBrush = Brushes.Gold;                  // Bulunan Yol (Sarı)

        private static readonly Pen GridPen = new Pen(Color.FromArgb(230, 230, 230), 1); // Çok ince ızgara çizgisi

        public static void Draw(Graphics g, GridState gridState)
        {
            if (gridState == null) return;

            // Tüm hücreleri gez ve durumuna göre boya
            foreach (var node in gridState.Nodes)
            {
                Brush currentBrush = GetBrushForState(node.State);

                // 1. Hücrenin içini boya (Yol ise PathBrush ile boyanacak)
                g.FillRectangle(currentBrush, node.Bounds);

                // 2. Hücre çerçevesini çiz (Izgara yapısını korumak için)
                g.DrawRectangle(GridPen, node.Bounds);
            }
        }

        private static Brush GetBrushForState(CellState state)
        {
            return state switch
            {
                CellState.Obstacle => ObstacleBrush,
                CellState.Start => StartBrush,
                CellState.End => EndBrush,
                CellState.Path => PathBrush,
                _ => EmptyBrush
            };
        }
    }
}