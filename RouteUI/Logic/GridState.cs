using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RouteUI.Models;

namespace RouteUI.Logic
{
    public class GridState
    {
        public int Width { get; }
        public int Height { get; }
        public int CellSize { get; private set; }
        private Size _availableSize;

        public List<NodeModel> Nodes { get; private set; }
        public NodeModel? StartNode { get; private set; }
        public NodeModel? EndNode { get; private set; }

        public GridState(int width, int height, Size availableSize)
        {
            Width = width;
            Height = height;
            _availableSize = availableSize;

            // 1. Dinamik Hücre Boyutu Hesabı (Margin düşülmüş hali)
            int cellW = (availableSize.Width - 40) / width;
            int cellH = (availableSize.Height - 40) / height;
            CellSize = Math.Max(2, Math.Min(cellW, cellH));

            Nodes = new List<NodeModel>();

            // 2. Izgarayı Oluştur
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int id = y * width + x;
                    // Koordinatları CellSize ile çarparak bounds oluşturuyoruz
                    Rectangle bounds = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);
                    Nodes.Add(new NodeModel(id, x, y, bounds));
                }
            }
        }

        public void SetStartNode(NodeModel node)
        {
            if (StartNode != null) StartNode.State = CellState.Empty;
            StartNode = node;
            if (node != null) node.State = CellState.Start;
        }

        public void SetEndNode(NodeModel node)
        {
            if (EndNode != null) EndNode.State = CellState.Empty;
            EndNode = node;
            if (node != null) node.State = CellState.End;
        }

        public NodeModel? GetNodeAt(Point location)
        {
            // Tıklanan koordinatı CellSize'a bölerek X ve Y indekslerini buluyoruz
            int x = location.X / CellSize;
            int y = location.Y / CellSize;

            // Sınır kontrolü (Index was out of range hatasını önler)
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                int id = (y * Width) + x;
                if (id >= 0 && id < Nodes.Count)
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

        public void ClearPath()
        {
            foreach (var node in Nodes)
            {
                if (node.State == CellState.Path || node.State == CellState.Visited)
                    node.State = CellState.Empty;
            }
        }
    }
}