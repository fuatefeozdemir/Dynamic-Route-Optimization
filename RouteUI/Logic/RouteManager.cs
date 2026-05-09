using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RouteUI.Interop;
using RouteUI.Models;

namespace RouteUI.Logic
{
    public class RouteManager : IDisposable
    {
        private IntPtr _graphPtr;
        public int Width { get; }
        public int Height { get; }

        public RouteManager(int width, int height)
        {
            Width = width;
            Height = height;
            _graphPtr = EngineBridge.CreateGraph(width, height);
        }

        public void ToggleObstacle(int id)
        {
            if (_graphPtr != IntPtr.Zero)
            {
                EngineBridge.ToggleObstacle(_graphPtr, id);
            }
        }

        public void BuildConnections()
        {
            if (_graphPtr != IntPtr.Zero)
            {
                EngineBridge.BuildConnections(_graphPtr);
            }
        }

        public (List<int> Path, List<int> Visited) FindPath(int startId, int endId, int queueType, out Metrics metrics)
        {
            metrics = new Metrics();
            if (_graphPtr == IntPtr.Zero) return (new List<int>(), new List<int>());

            IntPtr pathPtr = EngineBridge.FindPath(_graphPtr, startId, endId, queueType, ref metrics);

            List<int> pathList = new List<int>();
            List<int> visitedList = new List<int>();

            // 1. En Kısa Yolu Yakala
            if (metrics.RouteFound && metrics.PathLength > 0 && pathPtr != IntPtr.Zero)
            {
                int[] tempPath = new int[metrics.PathLength];
                Marshal.Copy(pathPtr, tempPath, 0, metrics.PathLength);
                pathList.AddRange(tempPath);
                EngineBridge.DeletePath(pathPtr);
            }

            // 2. Animasyon (Visited) Dizisini Yakala
            if (metrics.VisitedCount > 0 && metrics.VisitedNodes != IntPtr.Zero)
            {
                int[] tempVisited = new int[metrics.VisitedCount];
                Marshal.Copy(metrics.VisitedNodes, tempVisited, 0, metrics.VisitedCount);
                visitedList.AddRange(tempVisited);
                EngineBridge.DeleteVisitedNodes(metrics.VisitedNodes); // RAM sızıntısını önle
            }

            return (pathList, visitedList);
        }

        public void Dispose()
        {
            if (_graphPtr != IntPtr.Zero)
            {
                EngineBridge.DeleteGraph(_graphPtr);
                _graphPtr = IntPtr.Zero;
            }
        }
    }
}