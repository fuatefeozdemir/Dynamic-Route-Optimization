using System;
using System.Runtime.InteropServices;
using RouteUI.Models;

namespace RouteUI.Interop
{
    public static class EngineBridge
    {
        private const string DllName = "libRouteEngine.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateGraph(int width, int height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteGraph(IntPtr graph);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ToggleObstacle(IntPtr graph, int id);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void BuildConnections(IntPtr graph);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr FindPath(IntPtr graph, int startId, int endId, int queueType, ref Metrics outMetrics);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeletePath(IntPtr path);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteVisitedNodes(IntPtr visitedNodes);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GenerateRandomObstacles(IntPtr graph, int probabilityPercent, out int outCount);

        // Tüm engelleri sıfırlar
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClearAllObstacles(IntPtr graph);

        // Haritayı yeni boyutlarla RAM'de yeniden inşa eder
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetGraph(IntPtr graph, int newWidth, int newHeight);

        // C#daki diziyi doğrudan C++ pointerıyla eşler
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RemoveObstaclesBatch(IntPtr graph, int[] ids, int count);

        // C++'ın oluşturduğu int dizilerini bellekten silmek için Garbage Collector görevi görür
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeIntArray(IntPtr arrayPtr);
    }
}