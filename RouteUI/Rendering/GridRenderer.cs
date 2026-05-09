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
        private static readonly Brush EndBrush = Brushes.Crimson;
        private static readonly Brush PathBrush = Brushes.Gold;

        private static readonly Pen GridPen = new Pen(Color.FromArgb(230, 230, 230), 1);

        public static void Draw(Graphics g, GridState gridState)
        {
            if (gridState == null) return;

            foreach (var node in gridState.Nodes)
            {
                Brush currentBrush = GetBrushForState(node.State);

                g.FillRectangle(currentBrush, node.Bounds);
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