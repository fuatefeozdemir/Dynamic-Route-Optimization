using System;
using System.Collections.Generic;
using System.Drawing;
using RouteUI.Models;

namespace RouteUI.Logic
{
    public class GridState
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; private set; }
        public int PixelWidth { get; private set; }
        public int PixelHeight { get; private set; }

        public List<NodeModel> Nodes { get; private set; }
        public NodeModel? StartNode { get; private set; }
        public NodeModel? EndNode { get; private set; }

        public GridState(int width, int height, Size availableSize)
        {
            Width = width;
            Height = height;

            // Hücrelerin her zaman KARE kalması için X ve Y ekseninden en dar olanın oranı alınır.
            CellSize = Math.Min(availableSize.Width / (float)width, availableSize.Height / (float)height);

            PixelWidth = (int)(width * CellSize);
            PixelHeight = (int)(height * CellSize);

            Nodes = new List<NodeModel>(width * height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int id = y * width + x;

                    int px1 = (int)(x * CellSize);
                    int py1 = (int)(y * CellSize);
                    int px2 = (int)((x + 1) * CellSize);
                    int py2 = (int)((y + 1) * CellSize);

                    Rectangle bounds = new Rectangle(px1, py1, px2 - px1, py2 - py1);
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
            int x = (int)(location.X / CellSize);
            int y = (int)(location.Y / CellSize);

            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                int id = (y * Width) + x;
                return Nodes[id];
            }
            return null;
        }

        public NodeModel? GetNodeById(int id)
        {
            if (id >= 0 && id < Nodes.Count) return Nodes[id];
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