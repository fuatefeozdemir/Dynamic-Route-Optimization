using System.Drawing;
using RouteUI.Models;
using RouteUI.Logic;

namespace RouteUI.Rendering
{
    public static class GridRenderer
    {
        private static readonly Brush EmptyBrush = Brushes.White;
        private static readonly Brush ObstacleBrush = new SolidBrush(Color.FromArgb(45, 45, 45));
        private static readonly Brush StartBrush = Brushes.LimeGreen;
        private static readonly Brush EndBrush = Brushes.DodgerBlue;
        private static readonly Brush PathBrush = Brushes.Crimson;
        private static readonly Brush VisitedBrush = Brushes.LightSkyBlue;

        private static readonly Pen GridPen = new Pen(Color.FromArgb(200, 200, 200), 1);

        public static void Draw(Graphics g, GridState gridState)
        {
            if (gridState == null) return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            g.Clear(Color.White);

            foreach (var node in gridState.Nodes)
            {
                if (node.State != CellState.Empty)
                {
                    Brush currentBrush = GetBrushForState(node.State);
                    g.FillRectangle(currentBrush, node.Bounds);
                }
            }

            // Dikey Çizgiler
            for (int x = 0; x <= gridState.Width; x++)
            {
                int lineX = (int)(x * gridState.CellSize);
                g.DrawLine(GridPen, lineX, 0, lineX, gridState.PixelHeight);
            }

            // Yatay Çizgiler
            for (int y = 0; y <= gridState.Height; y++)
            {
                int lineY = (int)(y * gridState.CellSize);
                g.DrawLine(GridPen, 0, lineY, gridState.PixelWidth, lineY);
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
                CellState.Visited => VisitedBrush,
                _ => EmptyBrush
            };
        }
    }
}