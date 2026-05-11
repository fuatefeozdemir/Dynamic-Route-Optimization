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
        public int Width { get; private set; }
        public int Height { get; private set; }

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

        public List<int> GenerateRandomObstacles(int probabilityPercent)
        {
            List<int> obstacleIds = new List<int>();
            if (_graphPtr == IntPtr.Zero) return obstacleIds;

            int count;
            IntPtr arrayPtr = EngineBridge.GenerateRandomObstacles(_graphPtr, probabilityPercent, out count);

            if (count > 0 && arrayPtr != IntPtr.Zero)
            {
                int[] tempArray = new int[count];
                Marshal.Copy(arrayPtr, tempArray, 0, count);
                obstacleIds.AddRange(tempArray);

                // C++ tarafında üretilen int dizisini bellekten sil
                EngineBridge.FreeIntArray(arrayPtr);
            }

            return obstacleIds;
        }

        public void RemoveObstaclesBatch(List<int> ids)
        {
            if (_graphPtr != IntPtr.Zero && ids != null && ids.Count > 0)
            {
                int[] idArray = ids.ToArray();
                EngineBridge.RemoveObstaclesBatch(_graphPtr, idArray, idArray.Length);
            }
        }

        public void ClearAllObstacles()
        {
            if (_graphPtr != IntPtr.Zero)
            {
                EngineBridge.ClearAllObstacles(_graphPtr);
            }
        }

        public void ResetGraph(int newWidth, int newHeight)
        {
            if (_graphPtr != IntPtr.Zero)
            {
                Width = newWidth;
                Height = newHeight;
                EngineBridge.ResetGraph(_graphPtr, newWidth, newHeight);
            }
        }

        public (List<int> Path, List<int> Visited) FindPath(int startId, int endId, int queueType, out Metrics metrics)
        {
            metrics = new Metrics();
            if (_graphPtr == IntPtr.Zero) return (new List<int>(), new List<int>());

            IntPtr pathPtr = EngineBridge.FindPath(_graphPtr, startId, endId, queueType, ref metrics);

            List<int> pathList = new List<int>();
            List<int> visitedList = new List<int>();

            // En kısa yol
            if (metrics.RouteFound && metrics.PathLength > 0 && pathPtr != IntPtr.Zero)
            {
                int[] tempPath = new int[metrics.PathLength];
                Marshal.Copy(pathPtr, tempPath, 0, metrics.PathLength);
                pathList.AddRange(tempPath);
                EngineBridge.DeletePath(pathPtr);
            }

            // Visited düğümler
            if (metrics.VisitedCount > 0 && metrics.VisitedNodes != IntPtr.Zero)
            {
                int[] tempVisited = new int[metrics.VisitedCount];
                Marshal.Copy(metrics.VisitedNodes, tempVisited, 0, metrics.VisitedCount);
                visitedList.AddRange(tempVisited);
                EngineBridge.DeleteVisitedNodes(metrics.VisitedNodes);
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