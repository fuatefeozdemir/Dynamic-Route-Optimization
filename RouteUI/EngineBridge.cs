using System;
using System.Runtime.InteropServices; // DLL bağlantısı için şart

namespace RouteUI
{
    // C++ tarafındaki Metrics struct'ı ile birebir aynı sırada olmalı
    [StructLayout(LayoutKind.Sequential)]
    public struct Metrics
    {
        public long TimeMicroseconds;
        public int NodesExamined;
        public int PathLength;
        public bool RouteFound;
    }

    public static class EngineBridge
    {
        // DLL dosyasının adı (C++ tarafında derlediğinde bu isimde bir dosya çıkacak)
        private const string DllName = "libRouteEngine.dll";

        // C++: GridGraph* CreateGraph(int width, int height)
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateGraph(int width, int height);

        // C++: void DeleteGraph(GridGraph* graph)
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteGraph(IntPtr graph);

        // C++: int* FindPath(GridGraph* graph, int startId, int endId, int queueType, Metrics* outMetrics)
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr FindPath(IntPtr graph, int startId, int endId, int queueType, ref Metrics outMetrics);

        // C++: void DeletePath(int* path)
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeletePath(IntPtr path);
    }
}