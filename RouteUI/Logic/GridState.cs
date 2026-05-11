using System.Collections.Generic;
using System.Drawing;
using RouteUI.Models;

namespace RouteUI.Logic
{
    public class GridState
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }
        public float PixelWidth { get; }
        public float PixelHeight { get; }

        public List<NodeModel> Nodes { get; }
        public NodeModel? StartNode { get; private set; }
        public NodeModel? EndNode { get; private set; }

        public GridState(int width, int height, Size containerSize)
        {
            Width = width;
            Height = height;
            PixelWidth = containerSize.Width;
            PixelHeight = containerSize.Height;

            // Hücre boyutunu (CellSize) harita ekranına tam sığacak şekilde hesaplar
            float cellW = PixelWidth / Width;
            float cellH = PixelHeight / Height;
            CellSize = cellW < cellH ? cellW : cellH;

            Nodes = new List<NodeModel>(width * height);

            int id = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    RectangleF bounds = new RectangleF(x * CellSize, y * CellSize, CellSize, CellSize);
                    Nodes.Add(new NodeModel(id++, x, y, bounds));
                }
            }
        }

        // Tıklanan fare koordinatına (X, Y) denk gelen kareyi bulur
        public NodeModel? GetNodeAt(Point location)
        {
            int gridX = (int)(location.X / CellSize);
            int gridY = (int)(location.Y / CellSize);

            if (gridX >= 0 && gridX < Width && gridY >= 0 && gridY < Height)
            {
                int id = (gridY * Width) + gridX;
                return Nodes[id];
            }
            return null;
        }

        public NodeModel? GetNodeById(int id)
        {
            if (id >= 0 && id < Nodes.Count)
                return Nodes[id];
            return null;
        }

        public void SetStartNode(NodeModel? node)
        {
            if (StartNode != null) StartNode.State = CellState.Empty;
            StartNode = node;
            if (StartNode != null) StartNode.State = CellState.Start;
        }

        public void SetEndNode(NodeModel? node)
        {
            if (EndNode != null) EndNode.State = CellState.Empty;
            EndNode = node;
            if (EndNode != null) EndNode.State = CellState.End;
        }

        // Yeni bir algoritma test edilmeden önce ekrandaki eski mavi/kırmızı yolları siler
        public void ClearPath()
        {
            foreach (var node in Nodes)
            {
                if (node.State == CellState.Visited || node.State == CellState.Path)
                {
                    node.State = CellState.Empty;
                }
            }
        }
    }
}