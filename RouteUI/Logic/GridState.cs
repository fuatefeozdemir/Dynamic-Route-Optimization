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
        public int CellSize { get; }

        // C++ tarafındaki nodes dizisinin C# tarafındaki görsel yansıması
        public List<NodeModel> Nodes { get; private set; }

        // Başlangıç ve Bitiş noktalarını ID olarak değil, nesne olarak tutmak UI yönetimini kolaylaştırır
        public NodeModel StartNode { get; private set; }
        public NodeModel EndNode { get; private set; }

        public GridState(int width, int height, int cellSize)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            Nodes = new List<NodeModel>();

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // C++ constructor'ındaki (y * width + x) sırasıyla birebir aynı!
            int currentId = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // Her hücre için ekrandaki çizim alanını hesapla
                    Rectangle bounds = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);

                    Nodes.Add(new NodeModel(currentId++, x, y, bounds));
                }
            }
        }

        public void SetStartNode(NodeModel node)
        {
            // Eğer eski bir başlangıç varsa onu temizle
            if (StartNode != null) StartNode.State = CellState.Empty;

            StartNode = node;
            if (node != null) node.State = CellState.Start;
        }

        public void SetEndNode(NodeModel node)
        {
            // Eğer eski bir bitiş varsa onu temizle
            if (EndNode != null) EndNode.State = CellState.Empty;

            EndNode = node;
            if (node != null) node.State = CellState.End;
        }

        // Fare koordinatına göre hücre bulma (UI tıklamaları için)
        public NodeModel GetNodeAt(Point location)
        {
            // Basit matematiksel arama: O(1) hızında bulmak için koordinatı CellSize'a bölebiliriz
            int x = location.X / CellSize;
            int y = location.Y / CellSize;

            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                int id = (y * Width) + x;
                return Nodes[id];
            }
            return null;
        }

        // C++'tan gelen yol ID'lerini görselleştirmek için
        public NodeModel GetNodeById(int id)
        {
            if (id >= 0 && id < Nodes.Count)
                return Nodes[id];
            return null;
        }

        // Haritadaki tüm görsel "Yol" (Path) boyamalarını temizler (Yeni arama öncesi)
        public void ClearPath()
        {
            foreach (var node in Nodes)
            {
                if (node.State == CellState.Path)
                    node.State = CellState.Empty;
            }
        }
    }
}