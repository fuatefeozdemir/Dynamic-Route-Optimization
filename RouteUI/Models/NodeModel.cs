using System.Drawing;

namespace RouteUI.Models
{
    public class NodeModel
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public RectangleF Bounds { get; set; }

        public CellState State { get; set; } = CellState.Empty;

        public NodeModel(int id, int x, int y, RectangleF bounds)
        {
            Id = id;
            X = x;
            Y = y;
            Bounds = bounds;
        }
    }
}